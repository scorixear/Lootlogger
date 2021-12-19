using SUNLootLogger.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SUNLootLogger
{
    public class PacketHandler
    {
        private ILootService lootService;
        private HttpClient client;
        //private const string itemsMappingUrl = "https://kellerus.de/lootlogger/items.json";
        //private const string eventsMappingUrl = "https://kellerus.de/lootlogger/events.json";
        private bool isInitialized = false;
        private Dictionary<int, string> itemDictionary = new Dictionary<int, string>();
        private Dictionary<string, int> eventDictionary = new Dictionary<string, int>();
        SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        public PacketHandler(ILootService lootService)
        {
            this.lootService = lootService;
            client = new HttpClient();
        }
        
        public async void OnEvent(byte code, Dictionary<byte, object> parameters)
        {
            if (!this.isInitialized)
            {
                await this.InitializeAsync();
            }
            if (code == 2)
            {
                return;
            }
            object val;
            parameters.TryGetValue(252, out val);
            if (val == null)
            {
                return;
            }
            int iCode = 0;
            if (!int.TryParse(val.ToString(), out iCode))
            {
                return;
            }
            //Console.WriteLine(iCode + " - " + ((EventCodes)iCode).ToString());
            
            if (eventDictionary.ContainsKey("evOtherGrabbedLoot") && iCode == eventDictionary["evOtherGrabbedLoot"])
            {
                this.OnLootPicked(iCode, parameters);
            }
            else
            {
                this.DetectLootPacket(iCode, parameters);
            }

        }

        private void DetectLootPacket(int iCode, Dictionary<byte, object> parameters)
        {
            try
            {
                string looter = parameters[2].ToString();
                int quantity = int.Parse(parameters[5].ToString());
                int itemId = int.Parse(parameters[4].ToString());
                string itemName = itemDictionary[itemId];
                string deadPlayer = parameters[1].ToString();

                eventDictionary.Add("evOtherGrabbedLoot", iCode);
                var now = DateTime.Now;
                Console.WriteLine($"[{now:HH:mm:ss}] Detected Loot Event ID : {iCode}");
                OnLootPicked(iCode, parameters);
            } catch
            {

            }
        }

        private void OnLootPicked(int iCode, Dictionary<byte, object> parameters)
        {
            try
            {
                string looter = parameters[2].ToString();
                string quantity = parameters[5].ToString();
                int itemId = int.Parse(parameters[4].ToString());
                string itemName = itemDictionary[itemId];
                string deadPlayer = parameters[1].ToString();

                Loot loot = new Loot
                {
                    Id = itemId,
                    ItemName = itemName,
                    Quantity = int.Parse(quantity.ToString()),
                    PickupTime = DateTime.UtcNow,
                    BodyName = deadPlayer,
                    LooterName = looter
                };

                if (!loot.IsTrash)
                {
                    lootService.AddLootForPlayer(loot, looter);
                    string path = Path.Combine(Directory.GetCurrentDirectory(), "logs.txt");
                    var now = DateTime.Now;
                    string line = $"[{now:HH:mm:ss}] {looter} has looted {quantity}x {itemName} on {deadPlayer}";
                    Console.WriteLine(line);
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine(line);
                    }
                }
            }
            catch (Exception)
            {
                eventDictionary.Remove("evOtherGrabbedLoot");
            }
        }

        public class Item
        {
            public string Index { get; set; }
            public string UniqueName { get; set; }
        }

        public class Event
        {
            public string Name { get; set; }
            public int Code { get; set; }
        }

        

        private async Task InitializeAsync()
        {
            semaphore.Wait();
            try
            {
                if (itemDictionary.Count == 0)
                {
                    var response = await this.client.GetAsync(new Uri(Config.instance.ItemsUrl));
                    var content = await response.Content.ReadAsStringAsync();
                    List<Item> itemList = JsonConvert.DeserializeObject<List<Item>>(content);
                
                    itemList.ForEach(entry => 

                            itemDictionary.Add(int.Parse(entry.Index), entry.UniqueName)

                    );
                }

                if(eventDictionary.Count == 0)
                {
                    var response = await this.client.GetAsync(new Uri(Config.instance.EventsUrl));
                    var content = await response.Content.ReadAsStringAsync();
                    List<Event> eventList = JsonConvert.DeserializeObject<List<Event>>(content);
                    eventList.ForEach(entry => eventDictionary.Add(entry.Name, entry.Code));
                    this.isInitialized = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.StackTrace);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}

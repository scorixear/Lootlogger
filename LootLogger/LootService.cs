using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootLogger.Model;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Diagnostics;

namespace LootLogger
{
    public class LootService : ILootService
    {
        private const string url = "https://discord.com/api/webhooks/827877961754738698/irGvCn4H0KGV-t_pJ5jlyvTfO0tsutL3a6WswtcPSysuinJObqJyqzFGkHVUMCraeohA";

        private List<Player> players;
        private readonly HttpClient client;

        private DateTime lastUploadDate = DateTime.MinValue;

        public LootService()
        {
            players = new List<Player>();
            client = new HttpClient();
        }

        public void AddLootForPlayer(Loot loot, string playerName)
        {
            var existingPlayer = this.players.FirstOrDefault(p => p.Name == playerName);
            if (existingPlayer != null)
            {
                existingPlayer.Loots.Add(loot);
            }
            else
            {
                players.Add(new Player { Name = playerName, Loots = new List<Loot>() { loot } });
            }
        }

        public void SaveLootsToFile()
        {
            string content = JsonConvert.SerializeObject(this.players, Formatting.Indented);
            using (var fs = File.Create(Path.Combine(Directory.GetCurrentDirectory(), $"CombatLoots-{DateTime.Now.ToString("dd-MMM-HH-mm-ss")}.json")))
            {
                Byte[] bytes = new UTF8Encoding(true).GetBytes(content);
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public async void UploadLoots()
        {
            try
            {
                if (lastUploadDate.AddMinutes(1) > DateTime.UtcNow)
                {
                    Console.WriteLine("No more than 1 upload every 60 seconds");
                    return;
                }

                string content = JsonConvert.SerializeObject(this.players, Formatting.Indented);
                string subname = $"CombatLoots-{DateTime.Now.ToString("dd-MMM-HH-mm-ss")}.json";
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), subname);
                using (var fs = File.Create(fileName))
                {
                    Byte[] bytes = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(bytes, 0, bytes.Length);
                }

                if (content.Length < 1)
                {
                    Console.WriteLine("Nothing to post");
                    return;
                }

                MultipartFormDataContent form = new MultipartFormDataContent();
                var file_bytes = File.ReadAllBytes(fileName);
                form.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Document", subname);
                

                var response = await client.PostAsync(url, form);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Successfully uploaded logs");
                    this.lastUploadDate = DateTime.UtcNow;
                    string url = await response.Content.ReadAsStringAsync();
                    Process.Start(url);
                }
                else
                {
                    Console.WriteLine("Failed to upload logs");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Write(e.StackTrace);
            }
        }
    }
}

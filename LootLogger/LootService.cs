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
using Discord.Webhook;
using Discord;

namespace LootLogger
{
    public class LootService : ILootService
    {
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
            using (var fs = File.Create(Path.Combine(Directory.GetCurrentDirectory(), $"CombatLoots-{DateTime.UtcNow.ToString("dd-MMM-HH-mm-ss")}.json")))
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

                string content = JsonConvert.SerializeObject(players, Formatting.Indented);
                DateTime now = DateTime.UtcNow;
                string date = now.ToString("dd-MMM-HH-mm-ss");
                string subname = $"CombatLoots-{date}.json";
                string fileName = Path.Combine(Directory.GetCurrentDirectory(), subname);
                using (var fs = File.Create(fileName))
                {
                    Byte[] bytes = new UTF8Encoding(true).GetBytes(content);
                    fs.Write(bytes, 0, bytes.Length);
                }

                if (content.Length <= 2)
                {
                    Console.WriteLine("Nothing to post");
                    return;
                }

                var DCW = new DiscordWebhookClient(Config.instance.DiscordWebHook);
                using(var client = DCW)
                {
                    var eb = new EmbedBuilder();
                    eb.WithTitle("New LootLog ready!");
                    eb.WithDescription("Loot log has been created.");
                    eb.AddField("LootLog Date", now.ToString("dd/MMM HH:mm"));
                    eb.WithFooter($"Created by {Config.instance.Reporter}");
                    eb.WithColor(Color.Green);
                    Embed[] embedArray = new Embed[] { eb.Build() };
                    await DCW.SendFileAsync(embeds: embedArray, text:"", filePath: fileName);
                    Console.WriteLine("Successfully uploaded logs");
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.StackTrace);
            }
        }
    }
}

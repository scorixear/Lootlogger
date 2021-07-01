using Newtonsoft.Json;
using System.IO;
using System.Reflection;

namespace SUNLootLogger
{
    public class Config
    {
        public static string BaseLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static Config instance = Instantation();

        private static Config Instantation()
        {
            Config returnInstance;
            if (File.Exists(Path.Combine(BaseLocation, "Config.json")))
            {
                returnInstance = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Path.Combine(BaseLocation, "Config.json")));
            }
            else
            {
                returnInstance = new Config();
            }
            returnInstance.Save();
            return returnInstance;
        }

        private void Save()
        {
            File.WriteAllText(Path.Combine(BaseLocation, "Config.json"), JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        private Config() { }
        public string Reporter { get; set; } = "Scorix";
        public string ItemsUrl { get; set; } = "https://raw.githubusercontent.com/broderickhyman/ao-bin-dumps/master/formatted/items.json";
        public string EventsUrl { get; set; } = "http://guildofsun.com/json/events.json";
        public string DiscordWebHook { get; set; } = "https://discord.com/api/webhooks/827877961754738698/irGvCn4H0KGV-t_pJ5jlyvTfO0tsutL3a6WswtcPSysuinJObqJyqzFGkHVUMCraeohA";
    }
}

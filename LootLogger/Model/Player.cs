using Newtonsoft.Json;
using System.Collections.Generic;

namespace SUNLootLogger.Model
{
    public class Player
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public List<Loot> Loots { get; set; }
    }
}

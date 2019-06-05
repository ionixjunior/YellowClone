using System;
using Newtonsoft.Json;

namespace YellowClone.Models
{
    public class Trip
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }

        [JsonProperty("duration")]
        public string Duration { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("calories")]
        public double Calories { get; set; }

        [JsonProperty("co2")]
        public double Co2 { get; set; }

        [JsonProperty("directions")]
        public string Directions { get; set; }
    }
}

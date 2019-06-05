using System;
using Newtonsoft.Json;

namespace YellowClone.Models
{
    public class Account
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("photo")]
        public string Photo { get; set; }
    }
}

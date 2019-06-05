using System;
using Newtonsoft.Json;

namespace YellowClone.Models
{
    public class Wallet
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}

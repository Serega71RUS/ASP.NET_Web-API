using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Web_API.Models
{
    public class Сar
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("manufacturer")]
        public string manufacturer { get; set; }

        [JsonProperty("model")]
        public string model { get; set; }
    }
}

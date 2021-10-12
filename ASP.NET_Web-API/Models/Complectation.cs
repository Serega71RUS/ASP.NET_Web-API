using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Web_API.Models
{
    public class Complectation
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("model_id")]
        public int model_id { get; set; }

        [JsonProperty("engine_volume")]
        public double engine_volume { get; set; }

        [JsonProperty("power")]
        public int power { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class Complectation
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

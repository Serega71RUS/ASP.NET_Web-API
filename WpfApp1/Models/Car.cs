using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class Car
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("manufacturer")]
        public string manufacturer { get; set; }

        [JsonProperty("model")]
        public string model { get; set; }
    }
}

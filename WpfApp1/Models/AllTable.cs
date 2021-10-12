using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    class AllTable
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("Model")]
        public string Model { get; set; }

        [JsonProperty("ComplectID")]
        public int ComplectID { get; set; }

        [JsonProperty("Model_id")]
        public int Model_id { get; set; }

        [JsonProperty("Engine_volume")]
        public double Engine_volume { get; set; }

        [JsonProperty("Power")]
        public int Power { get; set; }
    }
}

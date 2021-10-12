using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Web_API.Models
{
    public class AllTable
    {
        public int Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public int ComplectID { get; set; }
        public int Model_id { get; set; }
        public double Engine_volume { get; set; }
        public int Power { get; set; }

        public AllTable(int id, string manufacturer, string model, int complectID, int model_id, double engine_volume, int power)
        {
            Id = id;
            Manufacturer = manufacturer;
            Model = model;
            ComplectID = complectID;
            Model_id = model_id;
            Engine_volume = engine_volume;
            Power = power;
        }

        public override bool Equals(object obj)
        {
            return obj is AllTable other &&
                   Id == other.Id &&
                   Manufacturer == other.Manufacturer &&
                   Model == other.Model &&
                   ComplectID == other.ComplectID &&
                   Model_id == other.Model_id &&
                   Engine_volume == other.Engine_volume &&
                   Power == other.Power;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Manufacturer, Model, ComplectID, Model_id, Engine_volume, Power);
        }
    }
}

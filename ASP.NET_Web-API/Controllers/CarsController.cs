using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
using ASP.NET_Web_API.Models;
using Newtonsoft.Json;
using System.Data.Entity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ASP.NET_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {

        SberCarsContext db;
        public CarsController(SberCarsContext context)
        {
            db = context;
            if (!db.car.Any())
            {
                db.SaveChanges();
            }
        }

        static public Complectation Round(Complectation complect)
        {
            complect.engine_volume = Math.Round(complect.engine_volume, 1);
            return complect;
        }

        // GET api/<CarsController>/5
        [HttpGet("Sber")]
        public JsonResult GetMan()
        {
            var AllManuf = from car in db.car
                           group car by car.manufacturer into g
                           select new { manufacturer = g.Key };
            return new JsonResult(AllManuf);
        }

        // GET api/<CarsController>/5
        [HttpGet("Sber/{man}")]
        public JsonResult GetModel(string man)
        {
            var Model = from car in db.car
                        where car.manufacturer == man
                        select new { car.model };

            return new JsonResult(Model);
        }

        // GET api/<CarsController>/5
        [HttpGet("Sber/{man}/{mod}")]
        public JsonResult GetEngine(string man, string mod)
        {
            var Engine = from car in db.car
                        join complectation in db.complectation on car.id equals complectation.model_id
                        where car.manufacturer == man & car.model == mod
                        select new { Round(complectation).engine_volume, complectation.power };
            return new JsonResult(Engine);
        }

        [HttpGet("All")]
        public JsonResult GetAll(string man, string mod)
        {
            var Engine = from car in db.car
                         join complectation in db.complectation on car.id equals complectation.model_id
                         select new AllTable(car.id, car.manufacturer, car.model, 
                         complectation.id, complectation.model_id, Round(complectation).engine_volume, complectation.power);
            return new JsonResult(Engine);
        }

        // GET api/<CarsController>/5
        [HttpGet("Sber/AllModel")]
        public JsonResult GetAllModel(string man)
        {
            var Model = from car in db.car
                        select new { car.model };

            return new JsonResult(Model);
        }

        // GET api/<CarsController>/5
        [HttpGet("Model_id/{mod}")]
        public JsonResult GetModelId(string mod)
        {
            var ModelId = from car in db.car
                          where car.model == mod
                         select new { car.id, car.manufacturer, car.model };
            return new JsonResult(ModelId);
        }

        // GET api/<CarsController>/5
        [HttpGet("Manuf_id/{man}")]
        public JsonResult GetManufId(string man)
        {
            var ManufId = from car in db.car
                          where car.manufacturer == man
                          select new { car.id, car.manufacturer, car.model };
            return new JsonResult(ManufId);
        }

        // GET api/<CarsController>/5
        [HttpGet("Manuf/{mod}")]
        public JsonResult GetManuf(string mod)
        {
            var Manuf = from car in db.car
                          where car.model == mod
                          select new { car.manufacturer };
            return new JsonResult(Manuf);
        }

        // GET api/<CarsController>/5
        [HttpGet("Complect_id/{Vol}/{Pow}")]
        public JsonResult GetModelId(double Vol, int Pow)
        {
            var ComplectId = from complectation in db.complectation
                          where complectation.engine_volume == Vol | complectation.power == Pow
                          select new { complectation.id };
            return new JsonResult(ComplectId);
        }

        // POST api/<Cars_Controller>
        [HttpPost("post/car")]
        public void PostCar([FromBody] string value)
        {
            Сar car = JsonConvert.DeserializeObject<Сar>(value);
            //string sql = $"insert into car values(nextval('caridseq'), '"+car.manufacturer+"', '"+car.model+"')";
            Console.WriteLine(db.car.Add(new Сar { id = db.car.Max(c => c.id)+1, manufacturer = car.manufacturer, model = car.model }).State);
            db.SaveChanges();
        }

        // POST api/<Cars_Controller>
        [HttpPost("post/complect")]
        public void PostComplectation([FromBody] string value)
        {
            Complectation complect = JsonConvert.DeserializeObject<Complectation>(value);
            int i = db.complectation.Max(c => c.id);
            Console.WriteLine(db.complectation.Add(new Models.Complectation 
            { 
                id = db.complectation.Max(c => c.id) + 1,
                model_id = complect.model_id,
                engine_volume = complect.engine_volume,
                power = complect.power
            }).State);
            db.SaveChanges();
        }

        // PUT api/<Cars_Controller>
        [HttpPut("put/complect")]
        public void PutComplect([FromBody] string value)
        {
            Complectation complect = JsonConvert.DeserializeObject<Complectation>(value);
            Console.WriteLine(db.complectation.Update(new Complectation 
            {
                id = complect.id,
                model_id = complect.model_id,
                engine_volume = complect.engine_volume,
                power = complect.power

            }).State);
            db.SaveChanges();
        }

        // PUT api/<Cars_Controller>
        [HttpPut("put/car")]
        public void PutCar([FromBody] string value)
        {
            Models.Сar car = JsonConvert.DeserializeObject<Models.Сar>(value);
            Console.WriteLine(db.car.Update(new Models.Сar
            {
                id = car.id,
                manufacturer = car.manufacturer,
                model = car.model

            }).State);
            db.SaveChanges();
        }

        // DELETE api/<Cars_Controller>/5
        [HttpDelete("delete/model/{id}")]
        public void DeleteModel(int id)
        {
            var model = new Models.Сar { id = id };
            db.Remove(model);
            db.SaveChanges();
        }

        // DELETE api/<Cars_Controller>/5
        [HttpDelete("delete/complect/{id}")]
        public void DeleteComplect(int id)
        {
            var model = new Models.Complectation { id = id };
            db.Remove(model);
            db.SaveChanges();
        }
    }
}

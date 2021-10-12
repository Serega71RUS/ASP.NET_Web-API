using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Web_API.Models
{
    public class SberCarsContext : DbContext
    {
        public DbSet<Сar> car { get; set; }
        public DbSet<Complectation> complectation { get; set; }
        public SberCarsContext(DbContextOptions<SberCarsContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

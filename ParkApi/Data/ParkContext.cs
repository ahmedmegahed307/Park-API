using Microsoft.EntityFrameworkCore;
using ParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Data
{
    public class ParkContext : DbContext
    {
        public ParkContext(DbContextOptions<ParkContext> options)
           : base(options)
        {
        }
        public DbSet<NationalPark> NationalParks { get; set; }
        public DbSet<Trail> Trails { get; set; }

    }
}

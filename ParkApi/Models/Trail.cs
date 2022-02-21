using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Models
{
    public class Trail
    {
        public int TrailId { get; set; }
        public string TrailName { get; set; }
        public string Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; } //foreign key
        public NationalPark NationalPark { get; set; }
        public DateTime DateCreated { get; set; }
        public double Elevation { get; set; }

    }
}

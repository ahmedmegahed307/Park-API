using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models
{
    public class Trail
    {
        public int TrailId { get; set; }
        public string TrailName { get; set; }
        public string Distance { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; } //foreign key
        public Park NationalPark { get; set; }
        public double Elevation { get; set; }
    }
}

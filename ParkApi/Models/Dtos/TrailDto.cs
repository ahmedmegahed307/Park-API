using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ParkAPI.Models.Trail;

namespace ParkAPI.Models.Dtos
{
    public class TrailDto
    {
        public int TrailId { get; set; }
        public string TrailName { get; set; }
        public string Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; } //foreign key
        public NationalParkDto NationalPark { get; set; }
        public double Elevation { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ParkAPI.Models.Trail;

namespace ParkAPI.Models.Dtos
{
    public class TrailCreateDto
    {
        //public int TrailId { get; set; } no need for it whil creating
        public string TrailName { get; set; }
        public string Distance { get; set; }
        public DifficultyType Difficulty { get; set; }
        public int NationalParkId { get; set; } //foreign key
                                                //public NationalParkDto NationalPark { get; set; } we don't need it so we can send only id while update-create
        public double Elevation { get; set; }
    }
}

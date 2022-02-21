using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Models
{
    public class NationalPark
    {
        public int NationalParkId { get; set; }
        [Required]
        public string ParkName { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Etablished { get; set; }
        public byte[] ParkImage { get; set; }

    }
}

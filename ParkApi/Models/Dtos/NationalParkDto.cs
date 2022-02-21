using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Models.Dtos
{
    public class NationalParkDto
    {

        public int NationalParkId { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Etablished { get; set; }
        public byte[] ParkImage { get; set; }

    }
}

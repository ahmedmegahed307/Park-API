using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Models
{
    public class Park
    {
        public int NationalParkId { get; set; }
        public string ParkName { get; set; }
        public string State { get; set; }
        public DateTime Created { get; set; }
        public DateTime Etablished { get; set; }
        public byte[] ParkImage { get; set; }
    }
}

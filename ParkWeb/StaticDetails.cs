using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb
{
    public static class StaticDetails
    {
        public static string APIBaseUrl = "https://localhost:44308/";
        public static string ParkPath = APIBaseUrl+"api/v1/Park/";
        public static string TrailPath =APIBaseUrl+ "api/v1/Trail/";
    }
}

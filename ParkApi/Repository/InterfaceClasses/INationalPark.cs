using ParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Repository.InterfaceClasses
{
    public interface INationalPark
    {
        List<NationalPark> GetNationalParks();
        NationalPark GetParkById(int id);
        bool NationalParkExist(string name);
        bool NationalParkExist(int id);
        bool CreatePark(NationalPark park);
        bool UpdatePark(NationalPark park);
        bool DeletePark(NationalPark park); // or id 
        bool Save();


    }
}

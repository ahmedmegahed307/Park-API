using ParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Repository.InterfaceClasses
{
    public interface ITrail
    {
        List<Trail> GetTrails();
        List<Trail> GetTrailsInPark(int parkid);
        Trail GetTrailById(int id);
        bool TrailExist(string name);
        bool TrailExist(int id);
        bool CreateTrail(Trail trail);
        bool UpdateTrail(Trail trail);
        bool DeleteTrail(Trail trail); // or id 
        bool Save();


    }
}

using ParkAPI.Data;
using ParkAPI.Models;
using ParkAPI.Repository.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkAPI.Repository.BL
{
    public class ClsNationalPark : INationalPark
    {
        ParkContext ctx;
        public ClsNationalPark(ParkContext context)
        {
            ctx = context;
        }
        public bool CreatePark(NationalPark park)
        {
            ctx.NationalParks.Add(park);
            return Save();

        }

        public bool DeletePark(NationalPark park)
        {
            ctx.NationalParks.Remove(park);
            return Save();
        }

        public List<NationalPark> GetNationalParks()
        {
            return ctx.NationalParks.OrderBy(a => a.ParkName).ToList();
        }

        public NationalPark GetParkById(int id)
        {
            
            return ctx.NationalParks.Where(a => a.NationalParkId == id).FirstOrDefault();
        }

        public bool NationalParkExist(string name)
        {
            bool value = ctx.NationalParks.Any(a => a.ParkName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool NationalParkExist(int id)
        {
            bool value = ctx.NationalParks.Any(a => a.NationalParkId == id);
            return value;
        }

        public bool Save()
        {
            return ctx.SaveChanges() >= 0 ? true : false;

        }

        public bool UpdatePark(NationalPark park)
        {
           ctx.NationalParks.Update(park);// ctx.Entry(park).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return Save();
        }
    }
}

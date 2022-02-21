using ParkAPI.Data;
using ParkAPI.Models;
using ParkAPI.Repository.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace trailAPI.Repository.BL
{
    public class ClsTrail : ITrail
    {
        ParkContext ctx;
        public ClsTrail(ParkContext context)
        {
            ctx = context;
        }
        public bool CreateTrail(Trail trail)
        {
            ctx.Trails.Add(trail);
            return Save();

        }

        public bool DeleteTrail(Trail trail)
        {
            ctx.Trails.Remove(trail);
            return Save();
        }

        public List<Trail> GetTrails()
        {
            return ctx.Trails.Include(a => a.NationalPark).OrderBy(a => a.TrailName).ToList();
        }

        public Trail GetTrailById(int id)
        {
            
            return ctx.Trails.Include(a => a.NationalPark).Where(a => a.TrailId == id).FirstOrDefault();
        }

        public bool TrailExist(string name)
        {
            bool value = ctx.Trails.Any(a => a.TrailName.ToLower().Trim() == name.ToLower().Trim());
            return value;
        }

        public bool TrailExist(int id)
        {
            bool value = ctx.Trails.Any(a => a.TrailId == id);
            return value;
        }

        public bool Save()
        {
            return ctx.SaveChanges() >= 0 ? true : false;

        }

        public bool UpdateTrail(Trail trail)
        {
           ctx.Trails.Update(trail);// ctx.Entry(trail).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            return Save();
        }
        public List<Trail> GetTrailsInPark(int parkid)
        {
            return ctx.Trails.Include(a=>a.NationalPark).Where(a=>a.NationalParkId==parkid).ToList();
        }
    }
}

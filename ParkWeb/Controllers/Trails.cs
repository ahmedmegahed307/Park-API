using Microsoft.AspNetCore.Mvc;
using ParkWeb.Models;
using ParkWeb.Models.ViewModel;
using ParkWeb.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class TrailsController : Controller
    {
        IPark _park;
        ITrail _trail;
        public TrailsController(IPark park,ITrail trail)
        {
            _park = park;
            _trail = trail;

        }
        public IActionResult Index()
        {
            return View(new Trail() { });
        }
        public async Task<IActionResult> GellAllTrails()
        {
            return Json(new { data = await _trail.GetAllAsync(StaticDetails.TrailPath) });
        }
       
        public async Task<IActionResult> UpSert(int? id)
        {
            IEnumerable<Park> parklist = await _park.GetAllAsync(StaticDetails.ParkPath);
            TrailVM objVM = new TrailVM()
            {
                ParkList = parklist.Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.ParkName,
                    Value = i.NationalParkId.ToString()
                }),
                trail= new Trail()
            };
           
            if (id == null) //for insert or create 
            {
                return View(objVM);
            }

            objVM.trail = await _trail.GetAsyc(StaticDetails.TrailPath, id.GetValueOrDefault());//update
            if (objVM.trail == null)
            {
                return NotFound();
            }
            return View(objVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpSert(TrailVM obj)
        {
            if (ModelState.IsValid)
            {
               
                if (obj.trail.TrailId == 0)
                {
                    await _trail.CreateAsync(StaticDetails.TrailPath, obj.trail);
                }
                else
                {
                    await _trail.UpdateAsyc(StaticDetails.TrailPath + obj.trail.TrailId, obj.trail);

                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(obj);
            }
        }
       [HttpDelete]
       public async Task<IActionResult> Delete(int id)
        {
            var status = await _trail.DeleteAsyc(StaticDetails.TrailPath, id);
            if(status )
            {
                return Json(new { success = true, message="Delete Successful" });
            }
            return Json(new { success = false, message = "Delete not Successful" });
        }
    }
}

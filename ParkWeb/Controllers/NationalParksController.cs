using Microsoft.AspNetCore.Mvc;
using ParkWeb.Models;
using ParkWeb.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ParkWeb.Controllers
{
    public class NationalParksController : Controller
    {
        IPark _park;
        public NationalParksController(IPark park)
        {
            _park = park;

        }
        public IActionResult Index()
        {
            return View(new Park() { });
        }
        public async Task<IActionResult> GetAllParks()
        {
            return Json(new { data = await _park.GetAllAsync(StaticDetails.ParkPath) });
        }
        public async Task<IActionResult> UpSert(int? id)
        {

            Park obj = new Park();
            if (id == null) //for insert or create 
            {
                return View(obj);
            }

            obj = await _park.GetAsyc(StaticDetails.ParkPath, id.GetValueOrDefault());//update
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpSert(Park obj)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using var ms1 = new MemoryStream();
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    obj.ParkImage = p1;
                }
                else
                {
                    var objFromDB = await _park.GetAsyc(StaticDetails.ParkPath, obj.NationalParkId);
                    obj.ParkImage = objFromDB.ParkImage;
                }
                if (obj.NationalParkId == 0)
                {
                    await _park.CreateAsync(StaticDetails.ParkPath, obj);
                }
                else
                {
                    await _park.UpdateAsyc(StaticDetails.ParkPath + obj.NationalParkId, obj);

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
            var status = await _park.DeleteAsyc(StaticDetails.ParkPath, id);
            if(status )
            {
                return Json(new { success = true, message="Delete Successful" });
            }
            return Json(new { success = false, message = "Delete not Successful" });
        }
    }
}

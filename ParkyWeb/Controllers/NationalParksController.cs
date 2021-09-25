using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class NationalParksController : Controller
    {
        private readonly INationalParkRepository _repo;

        public NationalParksController(INationalParkRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View(new NationalPark() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();
            if (id == null)
            {
                return View(nationalPark);
            }
            nationalPark = await _repo.GetAsync(StaticDetails.NationalParkAPIPath, id.GetValueOrDefault());
            if (nationalPark == null)
            {
                return NotFound();
            }
            return View(nationalPark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    //convert file to string
                    byte[] p1 = null;
                    using (var fileStream = files[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fileStream.CopyTo(ms);
                            p1 = ms.ToArray();
                        }
                    }
                    nationalPark.Picture = p1;
                }
                else
                {
                    //in case of no upload of image
                    var obj = await _repo.GetAsync(StaticDetails.NationalParkAPIPath, nationalPark.Id);
                    nationalPark.Picture = obj.Picture;
                }
                if (nationalPark.Id == 0)
                {
                    nationalPark.Created = DateTime.Now.Date;
                    await _repo.CreateAsync(StaticDetails.NationalParkAPIPath, nationalPark);
                }
                else
                {
                    await _repo.UpdateAsync(StaticDetails.NationalParkAPIPath + nationalPark.Id, nationalPark);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(nationalPark);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var status = await _repo.DeleteAsync(StaticDetails.NationalParkAPIPath, Convert.ToInt32(id));

                if (status)
                {
                    return Json(new { success = true, message = "Delete Successful." });
                }
                else
                {
                    return Json(new { success = false, message = "Delete Not Successful." });
                }
            }
            else
            {
                return Json(new { success = false, message = "Could not delete. Id is null." });
            }
        }

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _repo.GetAllAsync(StaticDetails.NationalParkAPIPath) });
        }
    }
}
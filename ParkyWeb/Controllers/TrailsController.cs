using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class TrailsController : Controller
    {
        private readonly ITrailRepository _trailsRepo;
        private readonly INationalParkRepository _nationalParkRepo;

        public TrailsController(ITrailRepository trailRepository, INationalParkRepository nationalParkRepository)
        {
            _trailsRepo = trailRepository;
            _nationalParkRepo = nationalParkRepository;
        }

        public IActionResult Index()
        {
            return View(new Trail() { });
        }

        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<NationalPark> npList = await _nationalParkRepo.GetAllAsync(StaticDetails.NationalParkAPIPath);

            TrailsVM trailsVM = new TrailsVM()
            {
                NationalParksList = npList.Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }),
                Trail=new Trail()
            };

            if (id == null)
            {
                return View(trailsVM);
            }

            trailsVM.Trail = await _trailsRepo.GetAsync(StaticDetails.TrailAPIPath, id.GetValueOrDefault());
            if (trailsVM.Trail == null)
            {
                return NotFound();
            }
            return View(trailsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailsVM trailsVM)
        {
            if (ModelState.IsValid)
            {
                if (trailsVM.Trail.Id == 0)
                {
                    await _trailsRepo.CreateAsync(StaticDetails.TrailAPIPath, trailsVM.Trail);
                }
                else
                {
                    await _trailsRepo.UpdateAsync(StaticDetails.TrailAPIPath + trailsVM.Trail.Id, trailsVM.Trail);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(trailsVM);
            }
        }

        public async Task<IActionResult> GetAllTrail()
        {
            return Json(new { data = await _trailsRepo.GetAllAsync(StaticDetails.TrailAPIPath) });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _trailsRepo.DeleteAsync(StaticDetails.TrailAPIPath, id);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            else
            {
                return Json(new { success = false, message = "Delete Not Successful" });
            }
        }
    }
}
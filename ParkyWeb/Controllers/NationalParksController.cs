using Microsoft.AspNetCore.Mvc;
using ParkyWeb.Models;
using ParkyWeb.Repository.IRepository;
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

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _repo.GetAllAsync(StaticDetails.NationalParkAPIPath) });
        }
    }
}
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

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await _repo.GetAllAsync(StaticDetails.NationalParkAPIPath) });
        }
    }
}
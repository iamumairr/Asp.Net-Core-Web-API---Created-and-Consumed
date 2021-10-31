using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ParkyWeb.Models;
using ParkyWeb.Models.ViewModels;
using ParkyWeb.Repository.IRepository;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ParkyWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INationalParkRepository _npRepo;
        private readonly ITrailRepository _trailRepo;

        public HomeController(ILogger<HomeController> logger,INationalParkRepository nationalParkRepository,ITrailRepository trailRepository)
        {
            _logger = logger;
            _npRepo = nationalParkRepository;
            _trailRepo = trailRepository;
        }

        public async Task<IActionResult> Index()
        {
            IndexViewModel ivm = new IndexViewModel()
            {
                NationalParks = await _npRepo.GetAllAsync(StaticDetails.NationalParkAPIPath),
                Trails = await _trailRepo.GetAllAsync(StaticDetails.TrailAPIPath)
            };
            return View(ivm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

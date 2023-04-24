using Microsoft.AspNetCore.Mvc;
using QBTourDuLich.Models;
using System.Diagnostics;
using X.PagedList;
using Microsoft.EntityFrameworkCore;

namespace QBTourDuLich.Controllers
{
    public class HomeController : Controller
    {
        QbdulichContext db= new QbdulichContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var top5tour = (from a in db.Tours where a.XepHangTour == 5 select a).Take(5).ToList();
            return View(top5tour);
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
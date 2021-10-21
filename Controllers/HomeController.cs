using CSVProssesing.Data;
using CSVProssesing.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CSVProssesing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private DataContext dataContext;

        public HomeController(ILogger<HomeController> logger, DataContext dataContext)
        {
            this.dataContext = dataContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(dataContext.People);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Error { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

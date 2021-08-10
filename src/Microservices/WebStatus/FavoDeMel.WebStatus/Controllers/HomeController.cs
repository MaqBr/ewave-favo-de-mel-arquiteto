using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace FavoDeMel.WebStatus.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var basePath = _configuration["BASE_PATH"];
            return Redirect($"{basePath}/healthz");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}

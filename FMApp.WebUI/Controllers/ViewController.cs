using Microsoft.AspNetCore.Mvc;

namespace FMApp.WebUI.Controllers
{
    public class ViewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
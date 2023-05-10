using Microsoft.AspNetCore.Mvc;

namespace Eys.Web.Main.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

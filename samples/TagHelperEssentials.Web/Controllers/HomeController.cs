using Microsoft.AspNet.Mvc;

namespace TagHelperEssentials.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

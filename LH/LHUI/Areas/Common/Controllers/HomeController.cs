using Microsoft.AspNetCore.Mvc;

namespace LHUI.Areas.Common.Controllers
{
    [Area("Common")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

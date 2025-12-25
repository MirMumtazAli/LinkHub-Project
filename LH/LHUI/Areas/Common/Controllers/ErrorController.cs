using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LHUI.Areas.Common.Controllers
{
    [Area("Common")]
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var exe = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            //log the exeception 
            //Write it to file
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }
    }
}

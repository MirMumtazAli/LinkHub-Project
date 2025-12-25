using LHDAL.DAos;
using LHDAL.UnitOfWork;
using LHUI.Areas.Admin.ViewModel;
using LHUI.Areas.User.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LHUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Admin,User")]
    public class BrowseLHUrlController : Controller
    {
        ILHDB _context;

        public BrowseLHUrlController(ILHDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var urlsDao = _context.lhUrlDb.GetAll(true);
            List<BrowseLHUrlsViewModel> urlvm = new List<BrowseLHUrlsViewModel>();

            foreach (var item in urlsDao)
            {
                urlvm.Add(new BrowseLHUrlsViewModel()
                {
                    UrlId = item.UrlId,
                    UrlTitle = item.UrlTitle,
                    LHUrlName = item.LHUrlName,
                    Description = item.Description,
                    CategoryName = item.Category!.CategoryName,
                    UserName = item.User!.UserName

                });
            }

            return View(urlvm);
        }
    }
}

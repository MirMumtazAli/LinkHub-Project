using LHDAL.DAos;
using LHDAL.UnitOfWork;
using LHUI.Areas.Admin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Policy;

namespace LHUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class ApproveLHUrlController : Controller
    {
        ILHDB _context;

        public ApproveLHUrlController(ILHDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            //var urlsDao = _context.lhUrlDb.GetAll(null);
            //var urlsDao = _context.lhUrlDb.GetAll(true);

            var urlsDao = _context.lhUrlDb.GetAll();
            List<ApproveLHURLsViewModel> urlvm = new List<ApproveLHURLsViewModel>();

            foreach (var item in urlsDao)
            {
                urlvm.Add(new ApproveLHURLsViewModel()
                {
                    UrlId = item.UrlId,
                    UrlTitle = item.UrlTitle,
                    LHUrlName = item.LHUrlName,
                    Description = item.Description,
                    CategoryName = item.Category!.CategoryName,
                    IsApproved = item.IsApproved,
                    UserName = item.User!.UserName

                });
            }

            return View(urlvm);
        }
        [HttpPost]
        public IActionResult GetByStatus(int status)
        {
            List<LHUrl> urlsDao = new List<LHUrl>();

            switch (status)
            {
                case 1:
                    urlsDao=_context.lhUrlDb.GetAll(null).ToList();
                    break;
                case 2:
                    urlsDao = _context.lhUrlDb.GetAll(true).ToList();
                    break;
                case 3:
                    urlsDao = _context.lhUrlDb.GetAll(false).ToList();
                    break;
                default:
                    break;
            }

            List<ApproveLHURLsViewModel> urlvm = new List<ApproveLHURLsViewModel>();

            foreach (var item in urlsDao)
            {
                urlvm.Add(new ApproveLHURLsViewModel()
                {
                    UrlId = item.UrlId,
                    UrlTitle = item.UrlTitle,
                    LHUrlName = item.LHUrlName,
                    Description = item.Description,
                    CategoryName = item.Category!.CategoryName,
                    IsApproved = item.IsApproved,
                    UserName = item.User!.UserName

                });
            }

            return View("Index",urlvm);
        }
        public IActionResult Approve(int Id)
        {
            _context.lhUrlDb.ApproveOrReject(Id,true);
            return RedirectToAction("Index");

        }
        public IActionResult Reject(int Id)
        {
            _context.lhUrlDb.ApproveOrReject(Id, false);
            return RedirectToAction("Index");

        }

        [HttpPost]
        public void ApproveAll(int[] urlIds)
        {
            foreach (var urlId in urlIds)
            {
                var lhUrl = _context.lhUrlDb.GetById(urlId);
                lhUrl.IsApproved = true;
                _context.lhUrlDb.Update(lhUrl);
            }
            return;
        }
    }
}

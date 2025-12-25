using LHDAL.DAos;
using LHDAL.UnitOfWork;
using LHUI.Areas.User.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace LHUI.Areas.User.Controllers
{
    [Area("User")]
    [Authorize(Roles = "Admin,User")]
    public class SubmitLHUrlController : Controller
    {

        ILHDB _context;
        UserManager<LHUser> _userManager;

        public SubmitLHUrlController(ILHDB context,UserManager<LHUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.categoryDb.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLHUrlViewModel model)
        {
            if (ModelState.IsValid)
            {
                LHUser user = await _userManager.GetUserAsync(User);
                LHUrl lhurl = new LHUrl()
                {
                    CategoryId = model.CategoryId,
                    Description = model.Description,
                    LHUrlName = model.LHUrlName,
                    UrlTitle = model.UrlTitle,
                    IsApproved = null,
                    Id= user.Id
                };

                _context.lhUrlDb.Create(lhurl);
                TempData["msg"] = "Created Successfully";
                return RedirectToAction("Create");
            }
            TempData["msg"] = "Creation Failed";

            ViewBag.CategoryId = new SelectList(_context.categoryDb.GetAll(), "CategoryId", "CategoryName");
            return View();
        }
    }
}

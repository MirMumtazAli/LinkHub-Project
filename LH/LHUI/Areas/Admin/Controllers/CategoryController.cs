using LHDAL.DAos;
using LHDAL.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LHUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        ILHDB _context;

        public CategoryController(ILHDB context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.categoryDb.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoryName","CategoryDescription")]Category category)
        {
            if (ModelState.IsValid)
            {
                _context.categoryDb.Create(category);
                return RedirectToAction("Create");

            }

            return View(category);

        }

        // GET: Admin/Category/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = _context.categoryDb.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CategoryId,CategoryName,CategoryDescription")] Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.categoryDb.Update(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Admin/Category/Details/5
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var category = _context.categoryDb.GetById(id.Value);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/Category/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var category = _context.categoryDb.GetById(id);
            if (category == null)
                return NotFound();

            return View(category); // passes category to Delete.cshtml
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _context.categoryDb.GetById(id);
            if (category == null)
                return NotFound();

            _context.categoryDb.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

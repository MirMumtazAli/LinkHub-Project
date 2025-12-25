using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LHDAL.DAos;
using LHDAL.Data;

namespace LHUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Category1Controller : Controller
    {
        private readonly LHDbContext _context;

        public Category1Controller(LHDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Category1
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                _context.SaveChanges();
                return Json(new { success = true, data = category });

            }
            return Json(new { success = false, message = "Invalid Data." });
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var Categories = _context.Categories.ToList();
            return Json(Categories);
        }


        [HttpGet]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            return Json(new { success = true, data = category });
        }

        [HttpPut]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return Json(new { success = true, data = category });
            }
            return Json(new { success = false, message = "Invalid Data." });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            _context.Remove(category);
            _context.SaveChanges();
            return Json(new { success = true, data = category });
        }


    }
}

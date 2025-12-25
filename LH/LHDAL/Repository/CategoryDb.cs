using LHDAL.DAos;
using LHDAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.Repository
{
    public class CategoryDb : ICategoryDb
    {
        LHDbContext LHDbContext;

        public CategoryDb(LHDbContext _LHDbContext)
        {
            LHDbContext = _LHDbContext;
        }

        public IEnumerable<Category> GetAll()
        {
            return LHDbContext.Categories;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await LHDbContext.Categories.ToListAsync();
        }
        public Category GetById(int id)
        {
            var category = LHDbContext.Categories.Find(id);
            return category!;
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            var category = await LHDbContext.Categories.FindAsync(id);
            return category!;
        }

        public bool Create(Category category)
        {
            LHDbContext.Add(category);
            LHDbContext.SaveChanges();
            return true;
        }
        public bool Update(Category category)
        {
            LHDbContext.Update(category);
            LHDbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var category = LHDbContext.Categories.Find(id);
            LHDbContext.Remove<Category>(category!);
            LHDbContext.SaveChanges();
            return true;
        }

    }
}

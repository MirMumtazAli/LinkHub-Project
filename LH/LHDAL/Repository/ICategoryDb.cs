using LHDAL.DAos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.Repository
{
    public interface ICategoryDb
    {
        IEnumerable<Category> GetAll();
        Task<IEnumerable<Category>> GetAllAsync();

        Category GetById(int id);
        Task<Category> GetByIdAsync(int id);

        bool Create(Category category);
        bool Update(Category category);
        bool Delete(int id);

    }
}

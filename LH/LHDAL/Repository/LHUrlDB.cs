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
    public class LHUrlDB : ILHUrlDB
    {
        LHDbContext LHDbContext;

        public LHUrlDB(LHDbContext _LHDbContext)
        {
            LHDbContext = _LHDbContext;
        }

        public IEnumerable<LHUrl> GetAll()
        {
            return LHDbContext.LHUrls.Include(x => x.Category)
                                        .Include(x => x.User); ;
        }

        public IEnumerable<LHUrl> GetAll(bool? IsApproved)
        {
            return LHDbContext.LHUrls.Where(x => x.IsApproved == IsApproved)
                                        .Include(x => x.Category)
                                        .Include(x => x.User);
        }
        public LHUrl GetById(int id)
        {
            var url = LHDbContext.LHUrls.Find(id);
            return url!;
        }
        public bool Create(LHUrl lHUrl)
        {
            LHDbContext.Add(lHUrl);
            LHDbContext.SaveChanges();
            return true;
        }
        public bool Update(LHUrl lHUrl)
        {
            LHDbContext.Update(lHUrl);
            LHDbContext.SaveChanges();
            return true;
        }
        public bool Delete(int id)
        {
            var url = LHDbContext.LHUrls.Find(id);
            LHDbContext.Remove<LHUrl>(url!);
            LHDbContext.SaveChanges();
            return true;
        }

        //public bool Approve(int id)
        //{ 
        //    var url = LHDbContext.LHUrls.Find(id);
        //    url.IsApproved = true;
        //    LHDbContext.Update(url);
        //    LHDbContext.SaveChanges();
        //    return true;

        //}
        //public bool Reject(int id)
        //{
        //    var url = LHDbContext.LHUrls.Find(id);
        //    url.IsApproved = false;
        //    LHDbContext.Update(url);
        //    LHDbContext.SaveChanges();
        //    return true;

        //}

        public bool ApproveOrReject(int id,bool isApproved)
        {
            var url = LHDbContext.LHUrls.Find(id);
            url.IsApproved = isApproved;
            LHDbContext.Update(url);
            LHDbContext.SaveChanges();
            return true;

        }

    }
}

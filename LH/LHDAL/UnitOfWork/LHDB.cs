using LHDAL.Data;
using LHDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.UnitOfWork
{
    public class LHDB : ILHDB, IDisposable
    {
        LHDbContext LHDbContext;
        public LHDB(LHDbContext _LHDbContext)
        {
            LHDbContext = _LHDbContext;
        }

        ICategoryDb _categoryDb;
        public ICategoryDb categoryDb
        {
            get
            {
                if (_categoryDb == null)
                {
                    _categoryDb = new CategoryDb(LHDbContext);
                }
                return _categoryDb;

                //return new CategoryDb(new LHDbContext());

            }
        }

        ILHUrlDB _lhUrlDb;
        public ILHUrlDB lhUrlDb
        {
            get
            {
                if (_lhUrlDb == null)
                {
                    _lhUrlDb = new LHUrlDB(LHDbContext);
                }
                return _lhUrlDb;

                //return new CategoryDb(new LHDbContext());

            }
        }

        public void Dispose()
        {
            LHDbContext.Dispose();
        }
    }
}

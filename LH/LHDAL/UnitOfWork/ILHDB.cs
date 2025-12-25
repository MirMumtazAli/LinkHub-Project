using LHDAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.UnitOfWork
{
    public interface ILHDB
    {
        ICategoryDb categoryDb { get; }
        ILHUrlDB lhUrlDb { get; }
    }
}

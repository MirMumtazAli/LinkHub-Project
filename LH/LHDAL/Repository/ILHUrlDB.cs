using LHDAL.DAos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LHDAL.Repository
{
    public interface ILHUrlDB
    {
        IEnumerable<LHUrl> GetAll();
        IEnumerable<LHUrl> GetAll(bool? IsApproved);

        LHUrl GetById(int id);
        bool Create(LHUrl lHUrl);
        bool Update(LHUrl lHUrl);
        bool Delete(int id);
        //bool Approve(int id);
        //bool Reject(int id);

        bool ApproveOrReject(int id, bool isApproved);

    }
}

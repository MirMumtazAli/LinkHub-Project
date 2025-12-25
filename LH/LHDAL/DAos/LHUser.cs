using Microsoft.AspNetCore.Identity;

namespace LHDAL.DAos
{
   
    public class LHUser : IdentityUser
    {
        public string? AlternateContact { set; get; }

        //public bool? IsActive { set; get; }

        public IEnumerable<LHUrl>? LHUrls { set; get; }
    }
}
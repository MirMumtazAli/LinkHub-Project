using LHDAL.DAos;

namespace LHUI.Areas.Admin.ViewModel
{
    public class ApproveLHURLsViewModel
    {
        public int UrlId { get; set; }
        public string? UrlTitle { get; set; }
        public string? LHUrlName { get; set; }
        public string? Description { get; set; }
        public bool? IsApproved { get; set; }
        public string? CategoryName { get; set; }
        public string? UserName { get; set; }


    }
}

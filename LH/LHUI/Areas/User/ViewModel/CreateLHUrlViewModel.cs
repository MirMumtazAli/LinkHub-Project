using LHDAL.DAos;
using System.ComponentModel.DataAnnotations;

namespace LHUI.Areas.User.ViewModel
{
    public class CreateLHUrlViewModel
    {
        [Required]
        public string? UrlTitle { get; set; }


        [Required]
        [Display(Name ="URL")]
        public string? LHUrlName { get; set; }



        public string? Description { get; set; }
        [Required]

        public int? CategoryId { get; set; }
       
    }
}

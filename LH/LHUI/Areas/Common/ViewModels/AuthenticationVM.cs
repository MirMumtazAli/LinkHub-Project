using System.ComponentModel.DataAnnotations;

namespace LHUI.Areas.Common.ViewModels
{
    public class RegistrationVM
    {
       

        [Required, EmailAddress]
        public string? UserEmail { get; set; }

        [Required]
        public string? Password { get; set; }


        [Required, Compare("Password")]
        public string? ConfirmPassword { get; set; }
    }

    public class LoginVM
    {
        

        [Required, EmailAddress]
        public string? UserEmail { get; set; }

        [Required]
        public string? Password { get; set; }

    }
}

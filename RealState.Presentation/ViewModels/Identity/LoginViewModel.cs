using System.ComponentModel.DataAnnotations;

namespace RealState.Presentation.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Paasword is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}

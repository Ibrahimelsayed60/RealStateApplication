using System.ComponentModel.DataAnnotations;

namespace RealState.Presentation.ViewModels.Identity
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Paasword is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Paasword is Required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        public string? Phonenumber { get; set; }

        public string? Role { get; set; }
    }
}

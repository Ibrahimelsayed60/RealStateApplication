using System.ComponentModel.DataAnnotations;

namespace RealState.Presentation.ViewModels.VillaVM
{
    public class EditVillaViewModel
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length for name is 100 characters")]
        [MinLength(10, ErrorMessage = "Minimum length for name is 10 characters")]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        [Display(Name = "Price Per night")]
        [Range(10, 1000)]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Area (sq ft)")]
        public int SquareFeet { get; set; }

        [Required]
        [Range(1, 10)]
        public int Occupancy { get; set; }

        public string? imageUrl { get; set; }

        public IFormFile? Image { get; set; }

    }
}

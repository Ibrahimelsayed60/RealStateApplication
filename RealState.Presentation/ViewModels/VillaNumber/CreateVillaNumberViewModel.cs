using System.ComponentModel;

namespace RealState.Presentation.ViewModels.VillaNumber
{
    public class CreateVillaNumberViewModel
    {
        [DisplayName("Room Number")]
        public int Villa_Number { get; set; }
        [DisplayName("Details about Room")]
        public string? SpecialDetails { get; set; }
        [DisplayName("Villa Name")]
        public int? VillaId { get; set; }

        public string? VillaName { get; set; }

    }
}

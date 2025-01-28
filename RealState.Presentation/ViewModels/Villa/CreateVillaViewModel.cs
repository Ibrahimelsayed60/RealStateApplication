namespace RealState.Presentation.ViewModels.Villa
{
    public class CreateVillaViewModel
    {

        public string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int SquareFeet { get; set; }

        public int Occupancy { get; set; }

        public IFormFile? ImageUrl { get; set; }

    }
}

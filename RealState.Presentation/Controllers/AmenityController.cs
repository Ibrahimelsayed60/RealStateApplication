using Microsoft.AspNetCore.Mvc;
using RealState.Application.Services;
using RealState.Domain.Services.Contract;

namespace RealState.Presentation.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IAmenityService _amenityService;

        public AmenityController(IAmenityService amenityService)
        {
            _amenityService = amenityService;
        }

        public async Task<IActionResult> Index()
        {
            var amenitiesData = await _amenityService.GetAllAmenities();
            return View(amenitiesData);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;

namespace RealState.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;

        public BookingController(IVillaService villaService)
        {
            _villaService = villaService;
        }

        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            Booking booking = new Booking() 
            {
                VillaId = villaId,
                Villa = await _villaService.GetVillaWithAmenityById(villaId),
                CheckInDate = checkInDate,
                NumberOfNights = nights,
                CheckOutDate = checkInDate.AddDays(nights)
            };

            return View(booking);
        }
    }
}

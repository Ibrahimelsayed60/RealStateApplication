using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using System.Security.Claims;

namespace RealState.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly UserManager<AppUser> _userManager;

        public BookingController(IVillaService villaService,
            UserManager<AppUser> userManager)
        {
            _villaService = villaService;
            _userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> FinalizeBooking(int villaId, DateOnly checkInDate, int nights)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            var user = await _userManager.FindByEmailAsync(userEmail);

            Booking booking = new Booking() 
            {
                VillaId = villaId,
                Villa = await _villaService.GetVillaWithAmenityById(villaId),
                CheckInDate = checkInDate,
                NumberOfNights = nights,
                CheckOutDate = checkInDate.AddDays(nights),
                BookingEmail = userEmail,
                PhoneNumber = user.PhoneNumber,
                BookingName = user.Name,
            };
            booking.TotalCost =  (double) booking.Villa.Price * nights;
            return View(booking);
        }
    }
}

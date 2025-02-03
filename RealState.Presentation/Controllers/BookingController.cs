using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using RealState.Presentation.Helpers;
using System.Security.Claims;

namespace RealState.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookingService _bookingService;

        public BookingController(IVillaService villaService,
            UserManager<AppUser> userManager,
            IBookingService bookingService)
        {
            _villaService = villaService;
            _userManager = userManager;
            _bookingService = bookingService;
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
                UserEmail = userEmail,
                PhoneNumber = user.PhoneNumber,
                BookingName = user.Name,
            };
            booking.TotalCost =  (double) booking.Villa.Price * nights;
            return View(booking);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> FinalizeBooking(Booking booking)
        {
            var villa = await _villaService.GetVillaByIdAsync(booking.VillaId);

            booking.TotalCost = (double)villa.Price * booking.NumberOfNights;

            booking.Status = SD.StatusPending;

            booking.BookingDate = DateTime.Now;

            var result = await _bookingService.CreateBookingAsync(booking);

            return RedirectToAction(nameof(BookingConfirmation), new {bookingId = booking.Id});
        }

        [Authorize]
        public IActionResult BookingConfirmation(int bookingId)
        {
            return View();
        }

    }
}

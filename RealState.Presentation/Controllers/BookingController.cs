using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Application.Services.interfaces;
using RealState.Domain.Entities;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using RealState.Presentation.Helpers;
using RealState.Presentation.ViewModels;
using Stripe.Checkout;
using System.Security.Claims;

namespace RealState.Presentation.Controllers
{
    public class BookingController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBookingService _bookingService;
        private readonly IPaymentService _paymentService;
        private readonly IMapper _mapper;

        public BookingController(IVillaService villaService,
            UserManager<AppUser> userManager,
            IBookingService bookingService,
            IPaymentService paymentService,
            IMapper mapper,
            IConfiguration configuration)
        {
            _villaService = villaService;
            _userManager = userManager;
            _bookingService = bookingService;
            _paymentService = paymentService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
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

            var domain = Request.Scheme + "://" + Request.Host.Value + "/";

            var options = _paymentService.CreateStripeSessionOptions(booking, villa, domain);

            var session = _paymentService.CreateSession(options);

            await _bookingService.UpdateStripePaymentId(booking.Id, session.Id, session.PaymentIntentId);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        [Authorize]
        public async Task<IActionResult> BookingConfirmation(int bookingId)
        {

            var booking = await _bookingService.GetBookingwithSpecById(bookingId);


            if (booking is not null)
            {
                if (booking.Status == SD.StatusPending)
                {
                    var service = new SessionService();
                    Session session = service.Get(booking.StripeSessionId);
                    if (session.PaymentStatus == "paid")
                    {
                        await _bookingService.UpdateStatus(bookingId, SD.StatusApproved);
                        await _bookingService.UpdateStripePaymentId(bookingId, session.Id, session.PaymentIntentId);
                    }
                } 
            }

            return View(bookingId);
        }

        #region API Endpoint
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<BookingViewModel> objBookings;

            

            if (User.IsInRole(SD.Role_Admin))
            {
                var bookings = await _bookingService.GetAllBookingswithSpec();

                objBookings = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingViewModel>>(bookings);

            }
            else
            {
                var userEmail = User.FindFirstValue(ClaimTypes.Email);

                var user = await _userManager.FindByEmailAsync(userEmail);

                var bookings = await _bookingService.GetBookingsByEmailWithSpec(userEmail);

                objBookings = _mapper.Map<IEnumerable<Booking>, IEnumerable<BookingViewModel>>(bookings);

            }
            return Json(new { data = objBookings });
        }

        #endregion

    }
}

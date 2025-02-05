using Microsoft.AspNetCore.Mvc;
using RealState.Application.Common;
using RealState.Domain;
using RealState.Domain.Services.Contract;
using RealState.Presentation.Helpers;
using RealState.Presentation.Models;
using RealState.Presentation.ViewModels;
using System;
using System.Diagnostics;

namespace RealState.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IBookingService _bookingService;

        public HomeController(
            IVillaService villaService,
            IVillaNumberService villaNumberService,
            IBookingService bookingService)
        {
            _villaService = villaService;
            _villaNumberService = villaNumberService;
            _bookingService = bookingService;
        }

        public async Task<IActionResult> Index()
        {
            var token = Request.Cookies["AuthToken"];

            HomeViewModel homeVM = new HomeViewModel()
            {
                VillaList =await _villaService.GetAllVillaWithAmenitySpecs(),
                NumberOfNights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };

            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> GetVillasByDate(int nights, DateOnly CheckInDate)
        {
            var villaList = await _villaService.GetAllVillaWithAmenitySpecs();

            var villaNumbersList = await _villaNumberService.GetAllVillaNumbers();

            var bookedVillas = await _bookingService.GetAllBookingsWithStatusSpecAsync(u => u.Status == SD.StatusApproved ||
            u.Status == SD.StatusCheckedIn);

            foreach (var villa in villaList)
            {
                var roomsAvailable = VillaAvailability.VillaRoomsAvailableCount(villa.Id, villaNumbersList.ToList(), CheckInDate, nights, bookedVillas.ToList());

                villa.isAvailable = roomsAvailable > 0? true : false;
            }
            HomeViewModel homeVM = new()
            {
                CheckInDate = CheckInDate,
                VillaList = villaList,
                NumberOfNights = nights
                
            };
            return PartialView("_villaList", homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using RealState.Domain;
using RealState.Domain.Services.Contract;
using RealState.Presentation.Models;
using RealState.Presentation.ViewModels;
using System;
using System.Diagnostics;

namespace RealState.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IVillaService _villaService;

        public HomeController(IVillaService villaService)
        {
            _villaService = villaService;
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

            foreach (var villa in villaList)
            {
                if (villa.Id % 2 == 0)
                {
                    villa.isAvailable = false;
                }
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

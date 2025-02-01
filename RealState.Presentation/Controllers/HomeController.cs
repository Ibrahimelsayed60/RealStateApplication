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
            HomeViewModel homeVM = new HomeViewModel()
            {
                VillaList =await _villaService.GetAllVillaWithAmenitySpecs(),
                NumberOfNights = 1,
                CheckInDate = DateOnly.FromDateTime(DateTime.Now)
            };

            return View(homeVM);
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

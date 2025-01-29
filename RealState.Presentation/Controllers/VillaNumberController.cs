using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Services.Contract;

namespace RealState.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaService;

        public VillaNumberController(IVillaNumberService villaService)
        {
            _villaService = villaService;
        }
        public async Task<IActionResult> Index()
        {
            var villasData = await _villaService.GetAllVillaNumbersWithVillaData();

            return View(villasData);
        }
    }
}

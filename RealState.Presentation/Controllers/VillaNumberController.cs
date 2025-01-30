using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using RealState.Presentation.ViewModels.VillaNumber;

namespace RealState.Presentation.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly IVillaNumberService _villaNService;
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VillaNumberController(
            IVillaNumberService villaService , 
            ILogger<VillaNumberController> logger,
            IWebHostEnvironment webHostEnvironment)
        {
            _villaNService = villaService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var villasData = await _villaNService.GetAllVillaNumbersWithVillaData();

            return View(villasData);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVillaNumberViewModel villaNumberVM)
        {
            if (!ModelState.IsValid)
            {
                return View(villaNumberVM);
            }

            var message = string.Empty;
            try
            {

                bool roomNumberExist = await _villaNService.CheckVillaNumberExists(villaNumberVM.Villa_Number);

                if (!roomNumberExist)
                {
                    var villaNum = new VillaNumber()
                    {
                        Villa_Number = villaNumberVM.Villa_Number,
                        SpecialDetails = villaNumberVM.SpecialDetails,
                        VillaId = villaNumberVM.VillaId.Value,
                    };

                    int result = _villaNService.CreateVillaNumber(villaNum);

                    if(result > 0)
                        return RedirectToAction(nameof(Index));

                    message = "Villa number did not be created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(villaNumberVM);


                }
                else
                {
                    message = "This Room is already exist";
                    ModelState.AddModelError(string.Empty, message);
                    return View(villaNumberVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An Error occured during Creating the Villa Number";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(villaNumberVM);

        }
    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Domain.Services.Contract;
using RealState.Presentation.ViewModels.Villa;

namespace RealState.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IGenericRepository<Villa> _villaRepo;
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<VillaController> _logger;

        public VillaController(IGenericRepository<Villa> villaRepo, 
            IAttachmentService attachmentService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<VillaController> logger)
        {
            _villaRepo = villaRepo;
            _attachmentService = attachmentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var villas = await _villaRepo.GetAllAsync();

            return View(villas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVillaViewModel createVillaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createVillaViewModel);
            }
            var message = string.Empty;

            try
            {
                var villa = new Villa()
                {
                    Name = createVillaViewModel.Name,
                    Description = createVillaViewModel.Description,
                    Price = createVillaViewModel.Price,
                    SquareFeet = createVillaViewModel.SquareFeet,
                    Occupancy = createVillaViewModel.Occupancy,
                    ImageUrl = await _attachmentService.UploadAsync(createVillaViewModel.ImageUrl, "images")
                };

                int result = _villaRepo.Add(villa);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Villa did not created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(createVillaViewModel);
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An Error occured during creating Employee";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(createVillaViewModel);

        }

    }
}

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Domain.Services.Contract;
using RealState.Presentation.ViewModels.VillaVM;

namespace RealState.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IAttachmentService _attachmentService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<VillaController> _logger;

        public VillaController(IVillaService villaService,
            IAttachmentService attachmentService,
            IWebHostEnvironment webHostEnvironment,
            ILogger<VillaController> logger)
        {
            _villaService = villaService;
            _attachmentService = attachmentService;
            _webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var villas = await _villaService.GetAllVillas();

            return View(villas);
        }

        #region Create Action
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                    ImageUrl = await _attachmentService.UploadAsync(createVillaViewModel.ImageUrl, @"VillaImage")
                };

                int result = _villaService.CreateVilla(villa);

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
        #endregion


        #region Update Action

        [HttpGet]
        public async Task<IActionResult> Update(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var villa = await _villaService.GetVillaById(Id.Value);

            if(villa is null)
            {
                return NotFound();
            }

            EditVillaViewModel villEdit = new EditVillaViewModel()
            {
                Id = villa.Id,
                Name = villa.Name,
                Description = villa.Description,
                Occupancy = villa.Occupancy,
                Price = villa.Price,
                SquareFeet = villa.SquareFeet,
                imageUrl = villa.ImageUrl,
            };

            return View(villEdit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromRoute] int id,EditVillaViewModel villEdit)
        {
            if(!ModelState.IsValid)
                return View(villEdit);

            var message = string.Empty;

            try
            {

                var villa = new Villa()
                {
                    Id = id,
                    Name = villEdit.Name,
                    Description = villEdit.Description,
                    Price = villEdit.Price,
                    Occupancy = villEdit.Occupancy,
                    SquareFeet = villEdit.SquareFeet,
                };
                villa.ImageUrl = villEdit.Image is null ? villEdit.imageUrl : await _attachmentService.UploadAsync(villEdit.Image, @"VillaImage");

                var result = _villaService.UpdateVilla(villa);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Villa did not Updated";
                    ModelState.AddModelError(string.Empty, message);
                    return View(villEdit);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An Error occured during updating Employee";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(villEdit);

        }


        #endregion

        #region Delete Action
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var villa = await _villaService.GetVillaById(id);
                bool result = false;

                if(villa is not null)
                    result = _villaService.DeleteVilla(villa);

                if (result )
                    return RedirectToAction(nameof(Index));
                message = "an error occured during Deleting Villa";

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during updating the Villa";
            }

            return RedirectToAction(nameof(Index));
        }


        #endregion

    }
}

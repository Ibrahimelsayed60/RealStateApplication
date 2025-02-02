using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RealState.Application.Common;
using RealState.Application.Services;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using RealState.Presentation.ViewModels.Amenity;

namespace RealState.Presentation.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class AmenityController : Controller
    {
        private readonly IAmenityService _amenityService;
        private readonly ILogger<AmenityController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AmenityController(IAmenityService amenityService, ILogger<AmenityController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _amenityService = amenityService;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var amenitiesData = await _amenityService.GetAllAmenities();
            return View(amenitiesData);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AmenityViewModel AmenityVM)
        {

            if(!ModelState.IsValid)
                return View(AmenityVM);

            var message = string.Empty;

            try
            {
                var amenity = new Amenity()
                {
                    Name = AmenityVM.Name,
                    Description = AmenityVM.Description,
                    VillaId = AmenityVM.VillaId.Value,
                };

                var result = _amenityService.CreateAmenity(amenity);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                message = "An Error occured during Creating Amenity";
                ModelState.AddModelError(string.Empty, message);
                return View(AmenityVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An Error Occured during Creating Amenity";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(AmenityVM);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if(id is null)
                return BadRequest();

            var amenity = await _amenityService.GetAmenityWirhSpecById(id.Value);

            if(amenity is null)
                return NotFound();


            var amenityVM = new AmenityViewModel()
            {
                Name = amenity.Name,
                Description = amenity.Description,
                VillaId = amenity.VillaId,
                VillaName = amenity.Villa.Name,
            };

            return View(amenityVM);
        }

        [HttpPost]
        public IActionResult Update([FromRoute] int id, AmenityViewModel amenityVM)
        {
            if(!ModelState.IsValid)
                return View(amenityVM);

            var message = string.Empty;

            try
            {

                var amenity = new Amenity()
                {
                    Id = id,
                    Name = amenityVM.Name,
                    Description = amenityVM.Description,
                    VillaId = amenityVM.VillaId.Value,
                };

                int result = _amenityService.UpdateAmenity(amenity);

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                message = "An error occured during Updating Amenity";
                ModelState.AddModelError(string.Empty, message);
                return View(amenityVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "An error occured during updating Amenity";
            }
            ModelState.AddModelError(string.Empty, message);
            return View(amenityVM);


        }



        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var amenity = await _amenityService.GetAmenityById(id);
                bool result;
                if(amenity is not null)
                {
                    result = await _amenityService.DeleteAmenity(amenity);

                    if(result)
                        return RedirectToAction(nameof(Index));

                    message = "an error occured during Deleting Amenity";
                }


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _webHostEnvironment.IsDevelopment() ? ex.Message : "an error has occured during Deleting the Room";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

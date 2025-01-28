using Microsoft.AspNetCore.Mvc;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;

namespace RealState.Presentation.Controllers
{
    public class VillaController : Controller
    {
        private readonly IGenericRepository<Villa> _villaRepo;

        public VillaController(IGenericRepository<Villa> villaRepo)
        {
            _villaRepo = villaRepo;
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

        //[HttpPost]
        //public IActionResult Create()
        //{

        //}

    }
}

using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Domain.Services.Contract;
using RealState.Domain.Specifications.VillaNumberSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IGenericRepository<VillaNumber> _vNumRepo;

        public VillaNumberService(IGenericRepository<VillaNumber> vNumRepo)
        {
            _vNumRepo = vNumRepo;
        }
        
        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbers()
        {
            return await _vNumRepo.GetAllAsync();
        }

        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithVillaData()
        {
            VillaNumberWithVillaSpecifications villaSpecs = new VillaNumberWithVillaSpecifications();

            return await _vNumRepo.GetAllWithSpecAsync(villaSpecs);
        }

        public async Task<VillaNumber?> GetVillaNumberByVilla_number(int villa_number)
        {
            var spec = new VillaNumberWithVillaSpecifications(villa_number);
            return await _vNumRepo.GetItemWithSpecAsync(spec);
        }

        public void CreateVillaNumber(VillaNumber villaNumber)
        {
            _vNumRepo.Add(villaNumber);
        }

        public void UpdateVillaNumber(VillaNumber villaNumber)
        {
            _vNumRepo.Update(villaNumber);
        }

        public bool DeleteVillaNumber(VillaNumber villaNumber)
        {
            return _vNumRepo.Delete(villaNumber) > 0;
        }

        public async Task<bool> CheckVillaNumberExists(int villa_number)
        {
            var data = await GetVillaNumberByVilla_number(villa_number);
            return data != null;
        }
    }
}

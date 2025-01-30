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

        public async Task<VillaNumber?> GetVillaNumberWithSpecById(int id)
        {
            VillaNumberWithVillaSpecifications specs = new VillaNumberWithVillaSpecifications(id);
            return await _vNumRepo.GetItemWithSpecAsync(specs);
        }

        public async Task<VillaNumber?> GetVillaNumberByVilla_number(int villa_number)
        {
            var villaspec = new VillaSpecParams(villa_number);
            var spec = new VillaNumberWithVillaSpecifications(villaspec);
            return await _vNumRepo.GetItemWithSpecAsync(spec);
        }

        public int CreateVillaNumber(VillaNumber villaNumber)
        {
            return _vNumRepo.Add(villaNumber);
        }

        public int UpdateVillaNumber(VillaNumber villaNumber)
        {
            return _vNumRepo.Update(villaNumber);
        }

        public bool DeleteVillaNumber(VillaNumber villaNumber)
        {
            return _vNumRepo.DeleteHard(villaNumber) > 0;
        }

        public async Task<bool> CheckVillaNumberExists(int villa_number)
        {
            var data = await GetVillaNumberByVilla_number(villa_number);
            return data != null;
        }
    }
}

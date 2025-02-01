using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using RealState.Domain.Specifications.VillaNumberSpecs;
using RealState.Domain.Specifications.VillaSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class VillaService : IVillaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Villa>> GetAllVillas()
        {
            return await _unitOfWork.Repository<Villa>().GetAllAsync();
        }

        public async Task<IEnumerable<Villa>> GetAllVillaWithAmenitySpecs()
        {
            var specs = new VillaWithAmenitySpecifications();

            return await _unitOfWork.Repository<Villa>().GetAllWithSpecAsync(specs);
        }
        public async Task<Villa> GetVillaById(int id)
        {
            return await _unitOfWork.Repository<Villa>().GetByIdAsync(id);
        }

        public int CreateVilla(Villa villa)
        {
            return _unitOfWork.Repository<Villa>().Add(villa);
        }

        public int UpdateVilla(Villa villa)
        {
            return _unitOfWork.Repository<Villa>().Update(villa);
        }

        public bool DeleteVilla(Villa villa)
        {
            return _unitOfWork.Repository<Villa>().DeleteSoft(villa) > 0;
        }

        
    }
}

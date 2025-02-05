using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Repositories.Contract;
using RealState.Domain.Services.Contract;
using RealState.Domain.Specifications.VillaNumberSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class VillaNumberService : IVillaNumberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VillaNumberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbers()
        {
            return await _unitOfWork.Repository<VillaNumber>().GetAllAsync();
        }

        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithVillaData()
        {
            VillaNumberWithVillaSpecifications villaSpecs = new VillaNumberWithVillaSpecifications();

            return await _unitOfWork.Repository<VillaNumber>().GetAllWithSpecAsync(villaSpecs);
        }

        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithSpecificCriteria(Expression<Func<VillaNumber, bool>> criteria)
        {
            VillaNumberWithVillaSpecifications villaSpecs = new VillaNumberWithVillaSpecifications(criteria);

            return await _unitOfWork.Repository<VillaNumber>().GetAllWithSpecAsync(villaSpecs);
        }

        public async Task<IEnumerable<VillaNumber>> GetAllVillaNumbersInSpecificVilla(int villaId)
        {
            var specs = new VillaNumbersByVillaIdSpecifications(villaId);
            return await _unitOfWork.Repository<VillaNumber>().GetAllWithSpecAsync(specs);
        }

        public async Task<VillaNumber?> GetVillaNumberWithSpecById(int id)
        {
            VillaNumberWithVillaSpecifications specs = new VillaNumberWithVillaSpecifications(id);
            return await _unitOfWork.Repository<VillaNumber>().GetItemWithSpecAsync(specs);
        }

        public async Task<VillaNumber?> GetVillaNumberByVilla_number(int villa_number)
        {
            var villaspec = new VillaSpecParams(villa_number);
            var spec = new VillaNumberWithVillaSpecifications(villaspec);
            return await _unitOfWork.Repository<VillaNumber>().GetItemWithSpecAsync(spec);
        }

        public int CreateVillaNumber(VillaNumber villaNumber)
        {
            return _unitOfWork.Repository<VillaNumber>().Add(villaNumber);
        }

        public int UpdateVillaNumber(VillaNumber villaNumber)
        {
            return _unitOfWork.Repository<VillaNumber>().Update(villaNumber);
        }

        public bool DeleteVillaNumber(VillaNumber villaNumber)
        {
            return _unitOfWork.Repository<VillaNumber>().DeleteHard(villaNumber) > 0;
        }

        public async Task<bool> CheckVillaNumberExists(int villa_number)
        {
            var data = await GetVillaNumberByVilla_number(villa_number);
            return data != null;
        }

        
    }
}

using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using RealState.Domain.Specifications.AmenitySpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AmenityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Amenity>> GetAllAmenities()
        {
            var spec = new AmenityForVillaSpecifications();

            return await _unitOfWork.Repository<Amenity>().GetAllWithSpecAsync(spec);
        }

        public async Task<Amenity> GetAmenityById(int id)
        {
            return await _unitOfWork.Repository<Amenity>().GetByIdAsync(id);
        }

        public async Task<Amenity> GetAmenityWirhSpecById(int id)
        {
            var spec = new AmenityForVillaSpecifications(id);
            return await _unitOfWork.Repository<Amenity>().GetItemWithSpecAsync(spec);
        }

        public int CreateAmenity(Amenity amenity)
        {
            return _unitOfWork.Repository<Amenity>().Add(amenity);
        }

        public async Task<bool> DeleteAmenity(Amenity amenity)
        {
            return _unitOfWork.Repository<Amenity>().DeleteSoft(amenity) > 0;
        }

        public int UpdateAmenity(Amenity amenity)
        {
            return _unitOfWork.Repository<Amenity>().Update(amenity);
        }

        
    }
}

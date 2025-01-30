using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IAmenityService
    {

        Task<IEnumerable<Amenity>> GetAllAmenities();

        Task<Amenity> GetAmenityById(int id);

        Task<Amenity> GetAmenityWirhSpecById(int id);

        int CreateAmenity(Amenity amenity);

        int UpdateAmenity(Amenity amenity);

        Task<bool> DeleteAmenity(Amenity amenity);

    }
}

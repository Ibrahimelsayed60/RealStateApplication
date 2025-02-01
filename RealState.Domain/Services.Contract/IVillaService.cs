using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IVillaService
    {

        Task<IEnumerable<Villa>> GetAllVillas();

        Task<IEnumerable<Villa>> GetAllVillaWithAmenitySpecs();

        Task<Villa> GetVillaById(int id);

        int CreateVilla(Villa villa);

        int UpdateVilla(Villa villa);

        bool DeleteVilla(Villa villa);

    }
}

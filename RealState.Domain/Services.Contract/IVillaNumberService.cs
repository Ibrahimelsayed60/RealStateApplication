using RealState.Domain.Entities;
using RealState.Domain.Specifications.VillaNumberSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IVillaNumberService
    {

        Task<IEnumerable<VillaNumber>> GetAllVillaNumbers();

        Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithVillaData(VillaNumberWithVillaSpecifications spec);

        Task<VillaNumber> GetVillaNumberByVilla_number(int villa_number);

        void CreateVillaNumber(VillaNumber villaNumber);

        void UpdateVillaNumber(VillaNumber villaNumber);

        bool DeleteVillaNumber(int villa_number);

        bool CheckVillaNumberExists(int villa_number);

    }
}

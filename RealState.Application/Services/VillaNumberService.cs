using RealState.Domain.Entities;
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
        public bool CheckVillaNumberExists(int villa_number)
        {
            throw new NotImplementedException();
        }

        public void CreateVillaNumber(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }

        public bool DeleteVillaNumber(int villa_number)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VillaNumber>> GetAllVillaNumbers()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<VillaNumber>> GetAllVillaNumbersWithVillaData(VillaNumberWithVillaSpecifications spec)
        {
            throw new NotImplementedException();
        }

        public Task<VillaNumber> GetVillaNumberByVilla_number(int villa_number)
        {
            throw new NotImplementedException();
        }

        public void UpdateVillaNumber(VillaNumber villaNumber)
        {
            throw new NotImplementedException();
        }
    }
}

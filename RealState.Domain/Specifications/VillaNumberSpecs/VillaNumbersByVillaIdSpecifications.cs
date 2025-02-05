using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Specifications.VillaNumberSpecs
{
    public class VillaNumbersByVillaIdSpecifications: BaseSpecifications<VillaNumber>
    {
        public VillaNumbersByVillaIdSpecifications(int villaId):base(v => v.VillaId == villaId)
        {
            
        }

    }
}

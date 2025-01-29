using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Specifications.VillaNumberSpecs
{
    public class VillaNumberWithVillaSpecifications : BaseSpecifications<VillaNumber>
    {

        public VillaNumberWithVillaSpecifications():base()
        {
            AddIncludes();
        }

        public VillaNumberWithVillaSpecifications(int villa_Number):base(v => v.Villa_Number == villa_Number)
        {
            AddIncludes();
        }


        public VillaNumberWithVillaSpecifications(VillaSpecParams spec)
            :base(v => (!spec.VillaNumber.HasValue ||  v.Villa_Number == spec.VillaNumber))
        {
            AddIncludes();
        }

        public void AddIncludes()
        {
            Includes.Add(v => v.Villa);
        }

    }
}

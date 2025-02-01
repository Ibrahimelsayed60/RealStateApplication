using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Specifications.VillaSpecs
{
    public class VillaWithAmenitySpecifications : BaseSpecifications<Villa>
    {

        public VillaWithAmenitySpecifications():base()
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(v => v.Amenities);
        }

    }
}

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

        public VillaWithAmenitySpecifications(int id):base(v => v.Id == id) 
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(v => v.Amenities);
        }

    }
}

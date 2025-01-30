using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Specifications.AmenitySpecs
{
    public class AmenityForVillaSpecifications : BaseSpecifications<Amenity>
    {

        public AmenityForVillaSpecifications():base()
        {
            AddIncludes();
        }

        public AmenityForVillaSpecifications(int id):base(a => a.Id == id) 
        {
            AddIncludes();
        }


        private void AddIncludes()
        {
            Includes.Add(a => a.Villa);
        }
    }
}

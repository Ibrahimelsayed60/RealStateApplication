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

        public VillaNumberWithVillaSpecifications(int id):base(v => v.Id == id) 
        {
            AddIncludes();
        }

        public void AddIncludes()
        {
            Includes.Add(v => v.Villa);
        }

    }
}

using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public VillaNumberWithVillaSpecifications(Expression<Func<VillaNumber, bool>> criteria): base(criteria)
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

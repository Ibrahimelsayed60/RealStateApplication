using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Entities
{
    public class VillaNumber :BaseModel
    {

        public int Villa_Number { get; set; }

        public string? SpecialDetails { get; set; }

        public Villa Villa { get; set; }

        public int VillaId { get; set; }

    }
}

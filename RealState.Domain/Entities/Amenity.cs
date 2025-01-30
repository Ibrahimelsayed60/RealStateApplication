using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Entities
{
    public class Amenity : BaseModel
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public Villa Villa { get; set; }

        public int VillaId { get; set; }
    }
}

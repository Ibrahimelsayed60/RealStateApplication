using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Entities
{
    public class Villa : BaseModel
    {
        public string Name { get; set; } 

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int SquareFeet { get; set; }

        public int Occupancy { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get;set; }

        public IEnumerable<Amenity>? Amenities { get; set; }

    }
}

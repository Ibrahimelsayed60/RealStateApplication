using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Specifications.BookingSpecs
{
    public class BookingForVillaSpecifications : BaseSpecifications<Booking>
    {
        public BookingForVillaSpecifications(int id):base( b => b.Id == id)
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            Includes.Add(b => b.Villa);
        }

    }
}

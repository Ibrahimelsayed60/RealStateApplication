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
        public BookingForVillaSpecifications():base()
        {
            AddIncludes();
        }

        public BookingForVillaSpecifications(string email):base(b => b.UserEmail == email)
        {
            AddIncludes();
        }

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

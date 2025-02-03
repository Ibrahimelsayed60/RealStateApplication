using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IBookingService
    {

        Task<int> CreateBookingAsync(Booking booking);

    }
}

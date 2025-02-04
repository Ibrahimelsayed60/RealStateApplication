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

        Task<IEnumerable<Booking>> GetAllBookingswithSpec();

        Task<IEnumerable<Booking>> GetBookingsByEmailWithSpec(string Email);

        Task<Booking?> GetBookingwithSpecById(int bookingId);

        Task<int> CreateBookingAsync(Booking booking);

        Task UpdateStatus(int bookingId, string orderStatus);

        Task UpdateStripePaymentId(int bookingId, string paymentSesionId, string paymentIntentId);

    }
}

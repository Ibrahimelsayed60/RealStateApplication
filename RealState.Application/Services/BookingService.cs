using RealState.Application.Common;
using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using RealState.Domain.Specifications.BookingSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateBookingAsync(Booking booking)
        {
            _unitOfWork.Repository<Booking>().Add(booking);
            return await _unitOfWork.CompleteAync();
        }

        public async Task<IEnumerable<Booking>> GetAllBookingswithSpec()
        {
            var spec = new BookingForVillaSpecifications();
            return await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
        }

        public async Task<IEnumerable<Booking>> GetAllBookingsWithStatusSpecAsync(Expression<Func<Booking, bool>> criteriaExpression)
        {
            var specs = new BookingForVillaSpecifications(criteriaExpression);

            return await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(specs);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByEmailWithSpec(string Email)
        {
            var spec = new BookingForVillaSpecifications(Email);
            return await _unitOfWork.Repository<Booking>().GetAllWithSpecAsync(spec);
        }

        public async Task<Booking?> GetBookingwithSpecById(int bookingId)
        {
            var spec = new BookingForVillaSpecifications(bookingId);
            return await _unitOfWork.Repository<Booking>().GetItemWithSpecAsync(spec);
        }

        public async Task UpdateStatus(int bookingId, string orderStatus)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);

            if(booking is not null)
            {
                booking.Status = orderStatus;

                if(booking.Status == StaticData.StatusCheckedIn)
                {
                    booking.ActualCheckInDate = DateTime.Now;
                }
                if(booking.Status == StaticData.StatusCompleted)
                {
                    booking.ActualCheckOutDate = DateTime.Now;
                }

                _unitOfWork.Repository<Booking>().Update(booking);
                await _unitOfWork.CompleteAync();
            }

        }

        public async Task UpdateStripePaymentId(int bookingId, string paymentSesionId, string paymentIntentId)
        {
            var booking = await _unitOfWork.Repository<Booking>().GetByIdAsync(bookingId);
            if (booking is not null)
            {
                if(!string.IsNullOrEmpty(paymentSesionId))
                {
                    booking.StripeSessionId = paymentSesionId;
                }
                if (!string.IsNullOrEmpty(paymentIntentId))
                {
                    booking.StripePaymentIntentId = paymentIntentId;
                    booking.PaymentDate = DateTime.Now;
                    booking.IsPaymentSuccessful = true;
                }

                _unitOfWork.Repository<Booking>().Update(booking);
                await _unitOfWork.CompleteAync();
            }
        }
    }
}

using RealState.Domain;
using RealState.Domain.Entities;
using RealState.Domain.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}

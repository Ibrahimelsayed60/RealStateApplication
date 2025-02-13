﻿using RealState.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Domain.Services.Contract
{
    public interface IBookingService
    {

        Task<IEnumerable<Booking>> GetAllBookingswithSpec();

        Task<IEnumerable<Booking>> GetAllBookingsWithStatusSpecAsync(Expression<Func<Booking, bool>> criteriaExpression);

        Task<IEnumerable<Booking>> GetBookingsByEmailWithSpec(string Email);

        Task<Booking?> GetBookingwithSpecById(int bookingId);

        Task<int> CreateBookingAsync(Booking booking);

        Task UpdateStatus(int bookingId, string orderStatus, int villaNumber);

        Task UpdateStripePaymentId(int bookingId, string paymentSesionId, string paymentIntentId);

    }
}

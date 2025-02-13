﻿using RealState.Domain.Entities;
using RealState.Domain.Entities.Identity;

namespace RealState.Presentation.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public string BookingEmail { get; set; }

        public string BookingName { get; set; }

        public string? PhoneNumber { get; set; }

        public Villa Villa { get; set; }

        public int VillaId { get; set; }

        public string UserEmail { get; set; }

        public double TotalCost { get; set; }

        public int NumberOfNights { get; set; }

        public string? Status { get; set; }

        public DateTime BookingDate { get; set; }

        public DateOnly CheckInDate { get; set; }

        public DateOnly CheckOutDate { get; set; }

        public bool IsPaymentSuccessful { get; set; } = false;
        public DateTime PaymentDate { get; set; }

        public string? StripeSessionId { get; set; }
        public string? StripePaymentIntentId { get; set; }

        public DateTime ActualCheckInDate { get; set; }
        public DateTime ActualCheckOutDate { get; set; }

        public int VillaNumber { get; set; }

        public AppUser  UserData { get; set; }
    }
}

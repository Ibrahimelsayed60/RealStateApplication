using Microsoft.Extensions.Configuration;
using RealState.Application.Services.interfaces;
using RealState.Domain.Entities;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services
{
    public class PaymentService : IPaymentService
    {
        public Session CreateSession(SessionCreateOptions options)
        {
            var service = new SessionService();

            Session session = service.Create(options);

            return session;

        }


        public SessionCreateOptions CreateStripeSessionOptions(Booking booking, Villa villa, string domain)
        {
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                SuccessUrl = domain + $"booking/BookingConfirmation?bookingId={booking.Id}",
                CancelUrl = domain + $"booking/FinalizeBooking?villaId={booking.VillaId}&checkInDate={booking.CheckInDate}&nights={booking.NumberOfNights}",
            };

            options.LineItems.Add(new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    UnitAmount = (long)(booking.TotalCost * 100),
                    Currency = "usd",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = villa.Name,
                        Images = new List<string> { "https://random-id.ngrok-free.app/files/images/VillaImage/"+villa.ImageUrl }
                    },
                },
                Quantity = 1,
            });

            return options;
        }

    }
}

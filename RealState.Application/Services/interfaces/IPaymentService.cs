using Microsoft.Extensions.Configuration;
using RealState.Domain.Entities;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Application.Services.interfaces
{
    public interface IPaymentService
    {
        Session CreateSession(SessionCreateOptions options);

        SessionCreateOptions CreateStripeSessionOptions(Booking booking, Villa villa, string domain);
    }
}

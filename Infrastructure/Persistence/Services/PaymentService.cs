using Application.Abstractions.Services;
using Application.Repositories.ReservationRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _stripeSecretKey = "sk_test_51Qn2HoJrd7Qcqp7a7OeAYumKj8KRYqfuXrampbOKl7hkAwJRffD7UqDgL4tv1s0xBmcerpXddi9ZfYkaBRObYT6x00Q9RAEqqQ";
        readonly IReservationReadRepository _reservationReadRepository;

        public PaymentService(IReservationReadRepository reservationReadRepository)
        {
            _reservationReadRepository = reservationReadRepository;
        }

        public async Task<Response<object>> Pay(string reservationId)
        {
            Reservation? reservation = await _reservationReadRepository.GetByIdAsync(reservationId);
            if (reservation == null)
            {
                new Response(ResponseStatusCode.NotFound, "Reservation not found.");
            }
            if(reservation.IsPayed)
            {
                return new Response<object>(ResponseStatusCode.ValidationError, "Reservation is already paid.");
            }

            decimal price = reservation.Room.PricePerNight;

            foreach (var item in reservation.ReservationServices)
            {
                price += item.Service.Price;
            }

            price *= (reservation.CheckOutDate - reservation.CheckInDate).Days;

            var options = new PaymentIntentCreateOptions
            {
                Amount =  Convert.ToInt64(price),
                Currency = "usd",
                PaymentMethodTypes = new List<string> { "card" },
            };
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent = await service.CreateAsync(options);
            return new Response<object>(ResponseStatusCode.Success, "Payment intent created successfully.")
            {
                Data = paymentIntent.ClientSecret
            };
        }
    }
}

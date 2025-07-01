using Application.ResponceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.TwiML.Voice;

namespace Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<Response<object>> Pay(string reservationId);
    }
}

using Application.ResponceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Application.Abstractions.Services
{
    public interface ISmsService
    {
        Task<Response> SendSmsAsync(string phoneNumber, string message);
    }
}

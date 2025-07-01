using Application.Abstractions.Services;
using Response = Application.ResponceObject.Response;
using Application.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Application.ResponceObject.Enums;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TwilloSmsService : ISmsService
    {
        readonly TwilioSettings _twilioSettings;

        public TwilloSmsService(IOptions<TwilioSettings> twilioSettings)
        {
            _twilioSettings = twilioSettings.Value;
        }

        public async Task<Response> SendSmsAsync(string phoneNumber, string message)
        {
            TwilioClient.Init(_twilioSettings.Sid, _twilioSettings.Token);
            var messageOptions = new CreateMessageOptions(new PhoneNumber(phoneNumber))
            {
                From = new PhoneNumber(_twilioSettings.Number),
                Body = message
            };
            var messageResource = await MessageResource.CreateAsync(messageOptions);
            if (messageResource.Status == MessageResource.StatusEnum.Sent || messageResource.Status == MessageResource.StatusEnum.Queued)
            {
                return new Response(ResponseStatusCode.Success, "SMS sent successfully.");
            }
            else if (messageResource.Status == MessageResource.StatusEnum.Failed)
            {
                return new Response(ResponseStatusCode.Error, "Failed to send SMS.");
            }
            else
            {
                return new Response(ResponseStatusCode.Error, "SMS sending status is unknown.");
            }
        }
    }
}

using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface ISmsService
    {
        Task<Response> SendSmsAsync(string phoneNumber, string message);
    }
}

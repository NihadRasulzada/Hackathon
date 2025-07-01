using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface IPaymentService
    {
        Task<Response<object>> Pay(string reservationId);
    }
}

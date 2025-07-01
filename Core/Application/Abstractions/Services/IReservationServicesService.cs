using Application.DTOs.ReservationServiceDTOs;
using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface IReservationServicesService
    {
        Task<Response> CreateReservationServiceAsync(CreateReservationServiceDTOs dto);
        Task<Response<IEnumerable<GetReservationServiceDTOs>>> GetAllReservationServicesAsync();
        Task<Response<GetReservationServiceDTOs>> GetReservationServiceByIdAsync(string id);
        Task<Response> DeleteReservationServiceAsync(string id);
    }
}

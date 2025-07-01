using Application.DTOs.ReservationDTOs;
using Application.ResponceObject;


namespace Application.Abstractions.Services
{
    public interface IReservationService
    {
        Task<Response> CreateReservationAsync(CreateReservationDTOs dto);
        Task<Response<IEnumerable<GetReservationDTOs>>> GetAllReservationsAsync();
        Task<Response<GetReservationDTOs>> GetReservationByIdAsync(string id);
        Task<Response> UpdateReservationAsync(string id, UpdateReservationDTOs dto);
        Task<Response> DeleteReservationAsync(string id);
        Task<Response<IEnumerable<GetReservationDTOs>>> GetAllSoftDeletedReservationsAsync();
        Task<Response> SoftDeleteReservationAsync(string id);
        Task<Response> GetReservationByIdAsyncSoftDelete(string id);
    }

}

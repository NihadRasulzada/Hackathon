using Application.DTOs.ReservationDTOs;
using Application.ResponceObject;
using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface IReservationService
    {
        Task<List<GetReservationDTOs>> GetAllReservationsAsync();
        Task<Reservation> CreateReservationAsync(CreateReservationDTOs reservationDTOs);
        Task<GetReservationDTOs> GetReservationByIdAsync(Guid id);
        Task<Response> UpdateCategoryAsync(Guid id, UpdateReservationDTOs categoryDTOs);
        Task<Response> DeleteCategoryAsync(Guid id);
    }
}

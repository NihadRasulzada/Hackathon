using Application.DTOs.ReservationServiceDTOs;
using Application.ResponceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

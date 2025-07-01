using Application.DTOs.ReservationDTOs;
using Application.DTOs.ServiceDTOs;
using Application.ResponceObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IServicesService
    {
        Task<Response> CreateServiceAsync(CreateServiceDTOs dto);
        Task<Response<IEnumerable<GetServiceDTOs>>> GetAllServicesAsync();
        Task<Response<GetServiceDTOs>> GetServiceByIdAsync(string id);
        Task<Response> UpdateServiceAsync(string id, UpdateServiceDTOs dto);
        Task<Response> DeleteServiceAsync(string id);
        Task<Response<IEnumerable<GetServiceDTOs>>> GetAllSoftDeletedServicesAsync();
        Task<Response> SoftDeleteServiceAsync(string id);
        Task<Response> GetServiceByIdAsyncSoftDelete(string id);
    }
}

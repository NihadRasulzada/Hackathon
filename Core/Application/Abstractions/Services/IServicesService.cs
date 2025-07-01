using Application.DTOs.ServiceDTOs;
using Application.ResponceObject;

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

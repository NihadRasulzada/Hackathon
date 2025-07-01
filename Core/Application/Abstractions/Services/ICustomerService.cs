using Application.DTOs.CustomerServiceDTOs;
using Application.ResponceObject;

namespace Application.Abstractions.Services
{
    public interface ICustomerService
    {
        Task<Response> CreateCustomerAsync(CreateCustomerDTO dto);
        Task<Response<IEnumerable<GetCustomerDTO>>> GetAllCustomersAsync();
        Task<Response<GetCustomerDTO>> GetCustomerByIdAsync(string customerId);
        Task<Response> UpdateCustomerAsync(UpdateCustomerDTO dto);
        Task<Response> DeleteCustomerAsync(string customerId);
        Task<Response<IEnumerable<GetCustomerDTO>>> GetAllSoftDeletedCustomersAsync();
        Task<Response> SoftDeleteCustomerAsync(string customerId);
    }
}

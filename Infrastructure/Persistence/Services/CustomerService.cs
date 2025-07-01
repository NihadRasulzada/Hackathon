using Application.Abstractions.Services;
using Application.DTOs.CustomerServiceDTOs;
using Application.Repositories.CustomerRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class CustomerService : ICustomerService
    {
        readonly ICustomeReadRepository _readRepository;
        readonly ICustomerWriteRepository _writeRepository;

        public CustomerService(ICustomeReadRepository readRepository, ICustomerWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<Response> CreateCustomerAsync(CreateCustomerDTO dto)
        {
            if (dto == null)
            {
                return new Response(ResponseStatusCode.NotFound, "Customer data cannot be null.");
            }
            bool result = await _writeRepository.AddAsync(new Customer
            {
                FinCode = dto.FinCode,
                FullName = dto.FullName,
                PhoneNumber = dto.PhoneNumber
            });
            if (!result)
            {
                return new Response(ResponseStatusCode.Error, "Failed to create customer.");
            }
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Customer created successfully.");
        }

        public async Task<Response> DeleteCustomerAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return new Response(ResponseStatusCode.NotFound, "Customer ID cannot be null or empty.");
            }
            var customer = await _readRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return new Response(ResponseStatusCode.NotFound, "Customer not found.");
            }
            bool result = await _writeRepository.RemoveAsync(customerId);
            if (!result)
            {
                return new Response(ResponseStatusCode.Error, "Failed to delete customer.");
            }
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Customer deleted successfully.");
        }

        public async Task<Response<IEnumerable<GetCustomerDTO>>> GetAllCustomersAsync()
        {
            IEnumerable<GetCustomerDTO> customers = await _readRepository.GetAll().Select(c => new GetCustomerDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                PhoneNumber = c.PhoneNumber,
                FinCode = c.FinCode
            }).ToListAsync();
            if (customers == null || !customers.Any())
            {
                return new Response<IEnumerable<GetCustomerDTO>>(ResponseStatusCode.NotFound, "No customers found.");
            }
            return new Response<IEnumerable<GetCustomerDTO>>(ResponseStatusCode.Success, "Customers retrieved successfully.")
            {
                Data = customers
            };
        }

        public async Task<Response<IEnumerable<GetCustomerDTO>>> GetAllSoftDeletedCustomersAsync()
        {
            IEnumerable<GetCustomerDTO> softDeletedCustomers = await _readRepository.GetAll().Where(c => c.IsDeleted == false)
                .Select(c => new GetCustomerDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    FinCode = c.FinCode
                }).ToListAsync();
            if (softDeletedCustomers == null || !softDeletedCustomers.Any())
            {
                return new Response<IEnumerable<GetCustomerDTO>>(ResponseStatusCode.NotFound, "No soft deleted customers found.");
            }
            return new Response<IEnumerable<GetCustomerDTO>>(ResponseStatusCode.Success, "Soft deleted customers retrieved successfully.")
            {
                Data = softDeletedCustomers
            };
        }

        public async Task<Response<GetCustomerDTO>> GetCustomerByIdAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return new Response<GetCustomerDTO>(ResponseStatusCode.NotFound, "Customer ID cannot be null or empty.");
            }
            var customer = await _readRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return new Response<GetCustomerDTO>(ResponseStatusCode.NotFound, "Customer not found.");
            }
            var customerDto = new GetCustomerDTO
            {
                Id = customer.Id,
                FullName = customer.FullName,
                PhoneNumber = customer.PhoneNumber,
                FinCode = customer.FinCode
            };
            return new Response<GetCustomerDTO>(ResponseStatusCode.Success, "Customer retrieved successfully.")
            {
                Data = customerDto
            };
        }

        public async Task<Response> SoftDeleteCustomerAsync(string customerId)
        {
            if (string.IsNullOrEmpty(customerId))
            {
                return new Response(ResponseStatusCode.NotFound, "Customer ID cannot be null or empty.");
            }
            var customer = await _readRepository.GetByIdAsync(customerId);
            if (customer == null)
            {
                return new Response(ResponseStatusCode.NotFound, "Customer not found.");
            }
            customer.IsDeleted = true; // Assuming IsDeleted is a property in Customer entity
            bool result = _writeRepository.Update(customer);
            if (!result)
            {
                return new Response(ResponseStatusCode.Error, "Failed to soft delete customer.");
            }
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Customer soft deleted successfully.");
        }

        public async Task<Response> UpdateCustomerAsync(UpdateCustomerDTO dto)
        {
            if (string.IsNullOrEmpty(dto.Id) || dto == null)
            {
                return new Response(ResponseStatusCode.NotFound, "Customer ID or data cannot be null or empty.");
            }
            var customer = await _readRepository.GetByIdAsync(dto.Id);
            if (customer == null)
            {
                return new Response(ResponseStatusCode.NotFound, "Customer not found.");
            }
            customer.FullName = dto.FullName;
            customer.PhoneNumber = dto.PhoneNumber;
            customer.FinCode = dto.FinCode;
            bool result = _writeRepository.Update(customer);
            if (!result)
            {
                return new Response(ResponseStatusCode.Error, "Failed to update customer.");
            }
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Customer updated successfully.");
        }
    }
}

using Application.Abstractions.Services;
using Application.DTOs.ServiceDTOs;
using Application.Repositories.ServiceRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class ServicesService : IServicesService
    {
        private readonly IServiceReadRepository _readRepository;
        private readonly IServiceWriteRepository _writeRepository;
        private readonly IMapper _mapper;

        public ServicesService(IServiceReadRepository readRepository, IServiceWriteRepository writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateServiceAsync(CreateServiceDTOs dto)
        {
            var existing = await _readRepository.Table.Where(s => s.Name == dto.Name).FirstOrDefaultAsync();
            if (existing != null)
                return new Response(ResponseStatusCode.Error, $"'{dto.Name}' adlı xidmət artıq mövcuddur.");

            var service = _mapper.Map<Service>(dto);
            service.Id = Guid.NewGuid().ToString();
            var result = await _writeRepository.AddAsync(service);
            if (!result)
                return new Response(ResponseStatusCode.Error, "Xidmət yaradılmadı.");

            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Xidmət uğurla yaradıldı.");
        }

        public async Task<Response<IEnumerable<GetServiceDTOs>>> GetAllServicesAsync()
        {
            var services = await _readRepository.GetAll().ToListAsync();
            var mapped = _mapper.Map<IEnumerable<GetServiceDTOs>>(services);
            return new Response<IEnumerable<GetServiceDTOs>>(ResponseStatusCode.Success, mapped);
        }

        public async Task<Response<GetServiceDTOs>> GetServiceByIdAsync(string id)
        {
            var service = await _readRepository.GetWhere(s => s.Id == id).FirstOrDefaultAsync();
            if (service == null)
                return new Response<GetServiceDTOs>(ResponseStatusCode.NotFound, $"ID-si {id} olan xidmət tapılmadı.");

            var dto = _mapper.Map<GetServiceDTOs>(service);
            return new Response<GetServiceDTOs>(ResponseStatusCode.Success, dto);
        }

        public async Task<Response> UpdateServiceAsync(string id, UpdateServiceDTOs dto)
        {
            var service = await _readRepository.GetWhere(s => s.Id == id).FirstOrDefaultAsync();
            if (service == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan xidmət tapılmadı.");

            _mapper.Map(dto, service);
            _writeRepository.Update(service);
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Xidmət uğurla yeniləndi.");
        }

        public async Task<Response> DeleteServiceAsync(string id)
        {
            var service = await _readRepository.GetWhere(s => s.Id == id).FirstOrDefaultAsync();
            if (service == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan xidmət tapılmadı.");

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Xidmət silindi.");
        }

        public async Task<Response<IEnumerable<GetServiceDTOs>>> GetAllSoftDeletedServicesAsync()
        {
            var services = await _readRepository.GetAll().Where(s => s.IsDeleted == false).ToListAsync();
            var mapped = _mapper.Map<IEnumerable<GetServiceDTOs>>(services);
            return new Response<IEnumerable<GetServiceDTOs>>(ResponseStatusCode.Success, mapped);
        }

        public async Task<Response> SoftDeleteServiceAsync(string id)
        {
            var service = await _readRepository.GetWhere(s => s.Id == id).FirstOrDefaultAsync();
            if (service == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan xidmət tapılmadı.");

            service.IsDeleted = true;
            _writeRepository.Update(service);
            await _writeRepository.SaveAsync();
            return new Response(ResponseStatusCode.Success, "Xidmət soft delete olundu.");
        }

        public async Task<Response> GetServiceByIdAsyncSoftDelete(string id)
        {
            var service = await _readRepository.GetWhere(s => s.Id == id && s.IsDeleted).FirstOrDefaultAsync();
            if (service == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan silinmiş xidmət tapılmadı.");

            var dto = _mapper.Map<GetServiceDTOs>(service);
            return new Response<GetServiceDTOs>(ResponseStatusCode.Success, dto);
        }
    }
}

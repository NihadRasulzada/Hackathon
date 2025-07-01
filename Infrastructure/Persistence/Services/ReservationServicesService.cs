using Application.Abstractions.Services;
using Application.DTOs.ReservationServiceDTOs;
using Application.Repositories.ReservationRepository;
using Application.Repositories.ServiceRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Services
{
    public class ReservationServicesService : IReservationServicesService
    {
        private readonly IReservationReadRepository _reservationReadRepository;
        private readonly IServiceReadRepository _serviceReadRepository;
        private readonly AppDbContext _context;

        public ReservationServicesService(
            IReservationReadRepository reservationReadRepository,
            IServiceReadRepository serviceReadRepository,
            AppDbContext context)
        {
            _reservationReadRepository = reservationReadRepository;
            _serviceReadRepository = serviceReadRepository;
            _context = context;
        }

        public async Task<Response> CreateReservationServiceAsync(CreateReservationServiceDTOs dto)
        {
            var reservation = await _reservationReadRepository
                .GetWhere(r => r.Id == dto.ReservationId)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {dto.ReservationId} olan rezervasiya tapılmadı.");

            var service = await _serviceReadRepository
                .GetWhere(s => s.Id == dto.ServiceId)
                .FirstOrDefaultAsync();

            if (service == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {dto.ServiceId} olan xidmət tapılmadı.");

            var reservationService = new ReservationService
            {
                ReservationId = dto.ReservationId,
                ServiceId = dto.ServiceId
            };

            await _context.ReservationServices.AddAsync(reservationService);
            await _context.SaveChangesAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya xidməti uğurla əlavə olundu.");
        }

        public async Task<Response> DeleteReservationServiceAsync(string id)
        {
            var reservationService = await _context.ReservationServices
                .FirstOrDefaultAsync(rs => rs.Id == id);

            if (reservationService == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya xidməti tapılmadı.");

            _context.ReservationServices.Remove(reservationService);
            await _context.SaveChangesAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya xidməti uğurla silindi.");
        }


        public async Task<Response<IEnumerable<GetReservationServiceDTOs>>> GetAllReservationServicesAsync()
        {
            var reservationServices = await _context.ReservationServices
                .Include(rs => rs.Reservation)
                .Include(rs => rs.Service)
                .ToListAsync();

            var dtos = reservationServices.Select(rs => new GetReservationServiceDTOs
            {
                Id = rs.Id,
                ReservationId = rs.ReservationId,
                ServiceId = rs.ServiceId
            });

            return new Response<IEnumerable<GetReservationServiceDTOs>>(ResponseStatusCode.Success, dtos);
        }


        public async Task<Response<GetReservationServiceDTOs>> GetReservationServiceByIdAsync(string id)
        {
            var rs = await _context.ReservationServices
                .Include(r => r.Reservation)
                .Include(r => r.Service)
                .FirstOrDefaultAsync(rs => rs.Id == id);

            if (rs == null)
                return new Response<GetReservationServiceDTOs>(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya xidməti tapılmadı.");

            var dto = new GetReservationServiceDTOs
            {
                Id = rs.Id,
                ReservationId = rs.ReservationId,
                ServiceId = rs.ServiceId
            };

            return new Response<GetReservationServiceDTOs>(ResponseStatusCode.Success, dto);
        }

    }

}

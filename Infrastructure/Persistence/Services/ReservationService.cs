using Application.Abstractions.Services;
using Application.DTOs.ReservationDTOs;
using Application.DTOs.ServiceDTOs;
using Application.Repositories;
using Application.Repositories.ReservationRepository;
using Application.ResponceObject;
using Application.ResponceObject.Enums;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Services
{
    public class ReservationServices : IReservationService
    {
        private readonly IReservationReadRepository _readRepository;
        private readonly IReservationWriteRepository _writeRepository;
        private readonly IMapper _mapper;


        public ReservationService(IReservationReadRepository readRepository, IReservationWriteRepository writeRepository, IMapper mapper)

        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }
        public async Task<Response> CreateReservationAsync(CreateReservationDTOs dto)
        {
            var reservation = _mapper.Map<Reservation>(dto);

            bool result = await _writeRepository.AddAsync(reservation);
            if (!result)
                return new Response(ResponseStatusCode.Error, "Rezervasiya əlavə edilə bilmədi.");

            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya uğurla əlavə olundu.");
        }


        public async Task<Response> DeleteReservationAsync(string id)
        {
            var reservation = await _readRepository
                .GetWhere(r => r.Id == id)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya tapılmadı.");

            await _writeRepository.RemoveAsync(id);
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya uğurla silindi.");
        }


        public async Task<Response<IEnumerable<GetReservationDTOs>>> GetAllReservationsAsync()
        {
            var reservations = await _readRepository.GetAll().ToListAsync();
            var reservationDTOs = _mapper.Map<List<GetReservationDTOs>>(reservations);

            return new Response<IEnumerable<GetReservationDTOs>>(ResponseStatusCode.Success, reservationDTOs);
        }

        public async Task<Response<IEnumerable<GetReservationDTOs>>> GetAllSoftDeletedReservationsAsync()
        {
            var deletedReservations = await _readRepository
                .GetWhere(r => r.IsDeleted == false)
                .ToListAsync();

            var reservationDtos = _mapper.Map<List<GetReservationDTOs>>(deletedReservations);

            return new Response<IEnumerable<GetReservationDTOs>>(ResponseStatusCode.Success, reservationDtos);
        }


        public async Task<Response<GetReservationDTOs>> GetReservationByIdAsync(string id)
        {
            var reservation = await _readRepository
                .GetWhere(r => r.Id == id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return new Response<GetReservationDTOs>(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya tapılmadı.");

            var dto = _mapper.Map<GetReservationDTOs>(reservation);
            return new Response<GetReservationDTOs>(ResponseStatusCode.Success, dto);
        }



        public async Task<Response> SoftDeleteReservationAsync(string id)
        {
            var reservation = await _readRepository
                .GetWhere(r => r.Id == id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya tapılmadı və ya artıq silinib.");

            reservation.IsDeleted = true;

            _writeRepository.Update(reservation);
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya uğurla silindi.");
        }


        public async Task<Response> UpdateReservationAsync(string id, UpdateReservationDTOs dto)
        {
            var reservation = await _readRepository
                .GetWhere(r => r.Id == id && r.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (reservation == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan rezervasiya tapılmadı.");

            _mapper.Map(dto, reservation);

            _writeRepository.Update(reservation);
            await _writeRepository.SaveAsync();

            return new Response(ResponseStatusCode.Success, "Rezervasiya uğurla yeniləndi.");
        }

        public async Task<Response> GetReservationByIdAsyncSoftDelete(string id)
        {
            var reservation = await _readRepository.GetWhere(s => s.Id == id && s.IsDeleted).FirstOrDefaultAsync();
            if (reservation == null)
                return new Response(ResponseStatusCode.NotFound, $"ID-si {id} olan silinmiş reservation tapılmadı.");

            var dto = _mapper.Map<GetReservationDTOs>(reservation);
            return new Response<GetReservationDTOs>(ResponseStatusCode.Success, dto);
        }


    }
}

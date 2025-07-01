using Application.Abstractions.Services;
using Application.DTOs.ReservationDTOs;
using Application.ResponceObject;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class ReservationService : IReservationService
    {
        public Task<Reservation> CreateReservationAsync(CreateReservationDTOs reservationDTOs)
        {
            throw new NotImplementedException();
        }

        public Task<Response> DeleteCategoryAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetReservationDTOs>> GetAllReservationsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetReservationDTOs> GetReservationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Response> UpdateCategoryAsync(Guid id, UpdateReservationDTOs categoryDTOs)
        {
            throw new NotImplementedException();
        }
    }
}

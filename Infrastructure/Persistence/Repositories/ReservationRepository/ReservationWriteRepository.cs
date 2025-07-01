using Application.Repositories.ReservationRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ReservationRepository
{
    public class ReservationWriteRepository : WriteRepository<Reservation>, IReservationWriteRepository
    {
        public ReservationWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

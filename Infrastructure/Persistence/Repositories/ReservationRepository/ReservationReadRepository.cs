using Application.Repositories.ReservationRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ReservationRepository
{
    public class ReservationReadRepository : ReadRepository<Reservation>, IReservationReadRepository
    {
        public ReservationReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}

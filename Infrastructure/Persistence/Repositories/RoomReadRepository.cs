using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class RoomReadRepository : ReadRepository<Room>, IRoomReadRepository
    {
        public RoomReadRepository(AppDbContext context) : base(context) { }
    }
}

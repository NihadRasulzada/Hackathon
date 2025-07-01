using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    internal class RoomWriteRepository : WriteRepository<Room>, IRoomWriteRepository
    {
        public RoomWriteRepository(AppDbContext context) : base(context) { }
    }
}

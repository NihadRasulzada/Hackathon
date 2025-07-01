using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.FileRepositories
{
    public class RoomImageReadRepositories : ReadRepository<RoomImage>, IRoomReadImageRepository
    {
        public RoomImageReadRepositories(AppDbContext context) : base(context)
        {
        }
    }
}

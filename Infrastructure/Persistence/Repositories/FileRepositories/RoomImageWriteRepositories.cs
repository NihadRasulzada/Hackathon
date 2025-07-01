using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.FileRepositories
{
    public class RoomImageWriteRepositories : WriteRepository<RoomImage>, IRoomImageWriteRepositories
    {
        public RoomImageWriteRepositories(AppDbContext context) : base(context)
        {
        }
    }
}

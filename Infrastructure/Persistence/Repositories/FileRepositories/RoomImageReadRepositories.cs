using Application.Repositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.FileRepositories
{
    public class RoomImageReadRepositories : ReadRepository<RoomImage>, IRoomReadImageRepository
    {
        public RoomImageReadRepositories(AppDbContext context) : base(context)
        {
        }
    }
}

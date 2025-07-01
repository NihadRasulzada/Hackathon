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
    public class RoomImageWriteRepositories:WriteRepository<RoomImage>,IRoomImageWriteRepositories
    {
        public RoomImageWriteRepositories(AppDbContext context) : base(context)
        {
        }
    }
}

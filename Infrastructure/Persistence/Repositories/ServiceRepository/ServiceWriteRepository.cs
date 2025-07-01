using Application.Repositories.ServiceRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ServiceRepository
{
    public class ServiceWriteRepository : WriteRepository<Service>, IServiceWriteRepository
    {
        public ServiceWriteRepository(AppDbContext context) : base(context)
        {
        }
    }
}

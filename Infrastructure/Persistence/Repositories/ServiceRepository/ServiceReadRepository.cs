using Application.Repositories.ServiceRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ServiceRepository
{
    public class ServiceReadRepository : ReadRepository<Service>, IServiceReadRepository
    {
        public ServiceReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using Application.Repositories.CustomerRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CustomerRepository
{
    public class CustomeReadRepository : ReadRepository<Customer>, ICustomeReadRepository
    {
        public CustomeReadRepository(AppDbContext context) : base(context)
        {
        }
    }
}

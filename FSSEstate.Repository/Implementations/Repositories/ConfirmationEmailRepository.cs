using FSSEstate.Repository.Context;
using FSSEstate.Repository.Entities;
using FSSEstate.Repository.Interfaces.Repositories;

namespace FSSEstate.Repository.Implementations.Repositories
{
    public class ConfirmationEmailRepository : GenericRepository<ConfirmationEmailEntity>, IConfirmationEmailRepository
    {
        public ConfirmationEmailRepository(AppDbContext dbContext) : base(dbContext)
        {
        } 
    }
}

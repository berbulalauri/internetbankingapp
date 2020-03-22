using BBS.DAL.Database;
using BBS.Models.Models;

namespace BBS.DAL.Repositories
{
    public class InterestPaymentTypeRepository : BaseRepository<InterestPaymentType>, IInterestPaymentTypeRepository
    {
        public InterestPaymentTypeRepository(BankDbContext ctx) : base(ctx)
        {
        }
    }
}
using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class LoanTypeRepository : BaseRepository<LoanType>, ILoanTypeRepository
    {
        public LoanTypeRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<LoanType>> GetAllLoanTypeWithCurrencyAsync()
        {
            return await _context.LoanTypes.Where(x => x.IsDeleted == false)
                .Include(x => x.Currency).Include(x => x.LoanRequests)
                .ThenInclude(x => x.Loan).AsNoTracking().ToListAsync();
        }

        public LoanType GetByIdAsync(int id)
        {
            var loanType = _context.LoanTypes.Include(x => x.Currency).FirstOrDefault(x => x.Id == id);
            return loanType;
        }

        public async Task<LoanType> GetLoanTypeWithCurrencyByIdAsync(int id)
        {
            return await _context.LoanTypes.Where(x => x.IsDeleted == false)
                .Include(x => x.Currency).Include(x => x.LoanRequests)
                .ThenInclude(x => x.Loan).FirstOrDefaultAsync(lt => lt.Id == id);
        }
    }
}

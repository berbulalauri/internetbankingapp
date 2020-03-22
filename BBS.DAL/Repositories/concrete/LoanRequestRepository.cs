using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class LoanRequestRepository : BaseRepository<LoanRequest>, ILoanRequestRepository
    {
        public LoanRequestRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<IEnumerable<LoanRequest>> GetAllWithFullInfo()
        {
            return await _context.LoanRequests.Where(x => !x.IsDeleted)
                .Include(x => x.Employment).ThenInclude(x => x.EmploymentType)
                .Include(x => x.LoanType).ThenInclude(x => x.Currency)
                .Include(x => x.AccrueAccount).Include(x => x.TransferAccount)
                .Include(x => x.User).ToListAsync();
        }

        public async Task<LoanRequest> GetByIdWithAllInfoAsync(int id)
        {
            return await _context.LoanRequests.Where(x => x.Id == id)
                .Include(x => x.LoanType).FirstOrDefaultAsync();
        }
    }
}


using BBS.DAL.Database;
using BBS.Models.Constants;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class LoanRepository : BaseRepository<Loan>, ILoanRepository
    {
        public LoanRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<int> GetTotalLoanCount()
        {
            var customerDepositCount = await _context.Loans.Where(x => x.IsDeleted == false).Select(x => x.Id).ToListAsync();
            var result = customerDepositCount.Count();
            return result;
        }
        public async Task<decimal> GetTotalLoanAmount()
        {
            var depositAmountList = await _context.Loans.Where(x => x.IsDeleted == false).Include(x=>x.LoanRequest).ToListAsync();
            decimal result = depositAmountList.Sum(x => x.LoanRequest.LoanSum);
            return result;
        }

        public Task<List<Loan>> GetAllLoans()
        {
            return _context.Loans.Where(x => x.IsDeleted == false)
                .Include(x => x.LoanRequest)
                .ThenInclude(x => x.LoanType)
                .ToListAsync();
        }
        public async Task<IEnumerable<Loan>> GetAllWithTrackingAsync()
        {
            return await _context.Loans.Where(x => !x.IsDeleted && 
                x.Status == LoanConstants.DefaultLoanStatus && x.RemainingLoanSum > 0)
                .Include(x => x.Account).Include(x => x.LoanRequest)
                .ThenInclude(x => x.AccrueAccount).ToListAsync();
        }
    }
}


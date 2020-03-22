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
    public class DepositRepository : BaseRepository<Deposit>, IDepositRepository
    {
        public DepositRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task Add(Deposit deposit)
        {
            await _context.AddAsync(deposit);
        }

        public async Task<int> GetTotalDepositCount()
        {
            var customerDepositCount = await _context.Deposits.Where(x => x.IsDeleted == false).Select(x => x.Id).ToListAsync();
            var result = customerDepositCount.Count();
            return result;
        }
        public async Task<decimal> GetTotalDepositAmount()
        {
            var depositAmountList = await _context.Deposits.Where(x => x.IsDeleted == false).ToListAsync();
            decimal result = depositAmountList.Sum(x => x.DepositAmount);
            return result;
        }

        public Task<List<Deposit>> GetAllWithTrackingAsync()
        {
            return _context.Deposits.Where(x => !x.IsDeleted)
                .Include(x => x.DepositType)
                .Include(x => x.DepositTerm)
                .Include(x => x.AccountForInterest)
                .Include(x => x.AccountToTransfer)
                .Include(x => x.AccountToTransfer)
                .Include(x => x.Currency)
                .Include(x => x.User)
                .ToListAsync();
        }
    }
}

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
    public class DepositTypeRepository : BaseRepository<DepositType>, IDepositTypeRepository
    {
        public DepositTypeRepository(BankDbContext ctx) : base(ctx)
        {
            
        }

        public async Task<List<DepositType>> GetAllWithTerm()
        {
            return await _context.DepositTypes.Where(x => x.IsDeleted == false).Include(x => x.DepositTerm).AsNoTracking().ToListAsync();
        }

        public async Task<List<DepositType>> GetAllWithCurrency()
        {
            return await _context.DepositTypes.Where(x=>x.IsDeleted == false).Include(x => x.Currency)
                .Include(x => x.InterestPaymentType)
                .Include(x=>x.Deposits)
                .Include(x=>x.DepositTerm).ToListAsync();
        }

        public async Task<DepositType> GetWithCurrency(int Id)
        {
            return await _context.DepositTypes.Where(x => x.Id == Id && x.IsDeleted == false)
                .Include(x => x.Currency)
                .Include(x => x.InterestPaymentType)
                .Include(x => x.DepositTerm)
                .FirstOrDefaultAsync();
        }

        public async Task<List<DepositType>> GetWithFilter(decimal amount, int currencyId, int termId)
        {
            IQueryable<DepositType> query = _context.DepositTypes;

            query = currencyId != 0 ? query.Where(x => x.CurrencyId == currencyId) : query;
            query = termId != 0 ? query.Where(x => x.DepositTermId == termId) : query;
            query = amount != 0 ? query.Where(x => x.MinimumDepositAmount <= amount && x.MaximumDepositAmount >= amount) : query;

            return await query.Include(x => x.Currency).ToListAsync();
        }
        public async Task<DepositType> GetDepositTypeRepository(int Id)
        {
            return await _context.DepositTypes.FindAsync(Id);
        }

        public async Task<IQueryable<DepositType>> GetDepositQuerry() 
        {
            IQueryable<DepositType> query = _context.DepositTypes;

            return query.Where(x => x.IsDeleted == false).Include(x => x.Currency)
                .Include(x => x.InterestPaymentType)
                .Include(x => x.Deposits)
                .Include(x => x.DepositTerm);
        }
    }
}


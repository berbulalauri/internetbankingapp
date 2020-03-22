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
    public class TransactionRepository : BaseRepository<Transactions>, ITransactionsRepository
    {
        public TransactionRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<Transactions>> TransactionsWithInclides()
        {
            return await _context.Transactions
                .Include(x => x.Service)
                .Include(x => x.SenderAccount)
                .Include(x => x.ReceiverAccount)
                .Include(x => x.SenderCard)
                .ToListAsync();
        }

        public async Task<List<Transactions>> TransactionsWithInclides(int userId)
        {
            return await _context.Transactions.Include(x => x.Service).Include(x => x.SenderCard).Include(x => x.SenderAccount).ThenInclude(x => x.Currency)
                .Include(x => x.ReceiverAccount).Where(x => x.SenderCard.UserId == userId || x.ReceiverAccount.UserId == userId).AsNoTracking().ToListAsync();
        }
    }
}


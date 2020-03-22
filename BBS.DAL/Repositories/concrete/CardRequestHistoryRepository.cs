using BBS.DAL.Database;
using BBS.DAL.Repositories.abstractions;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.concrete
{
    public class CardRequestHistoryRepository : BaseRepository<CardRequestHistory>, ICardRequestHistoryRepository
    {
        public CardRequestHistoryRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<List<CardRequestHistory>> GetAllByRequestIdIncludesAsync(int id)
        {
            return await _context.CardRequestHistory
                .Where(x => x.IsDeleted == false && x.CardRequestId == id)
                .Include(x => x.Card)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
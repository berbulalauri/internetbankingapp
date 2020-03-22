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
    public class CardRequestRepository : BaseRepository<CardRequest>, ICardRequestRepository
    {
        public CardRequestRepository(BankDbContext ctx) : base(ctx)
        {
        }
        public async Task<List<CardRequest>> GetAllIncludesAsync()
        {
            return await _context.CardRequests
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User)
                .ThenInclude(x=>x.City)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<CardRequest> GetByIdIncludesAsync(int id)
        {
            return await _context.CardRequests
                .Where(x => x.IsDeleted == false && x.Id==id)
                .Include(x => x.User)
                .ThenInclude(x=>x.City)
                .FirstOrDefaultAsync();
        }

        public async Task<CardRequest> GetByUserIdAsync(int? id)
        {
            return await _context.CardRequests
                .Where(x => x.IsDeleted == false && id == x.UserId)
                .Include(x => x.User)
                .ThenInclude(x => x.City)
                .FirstOrDefaultAsync();
        }
    }
}

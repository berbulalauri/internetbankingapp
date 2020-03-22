using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class CardRepository : BaseRepository<Card>, ICardRepository
    {
        public CardRepository(BankDbContext ctx) : base(ctx)
        {
        }
        public async Task<Card> GetCardAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<List<Card>> GetCardsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<List<Card>> GetCardsIncludesAsync()
        {
            return await _context.Cards.Where(x => x.IsDeleted == false).Include(x => x.Account).ThenInclude(x => x.Currency).ToListAsync();
        }

        public async Task<List<Card>> GetCardsIncludesAsync(int userId)
        {
            return await _context.Cards.Where(x => x.IsDeleted == false && x.UserId == userId).Include(x => x.Account).ThenInclude(x => x.Currency).AsNoTracking().ToListAsync();
        }

        public async Task<Card> GetCardIncludesAsync(int id)
        {
            return await _context.Cards.Where(x => x.IsDeleted == false && x.Id == id).Include(x => x.Account).ThenInclude(x => x.Currency).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByUserIdAsync(int userId)
        {
            return await _context.Cards.Where(x => x.IsDeleted == false && x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public int GetSenderAccountIdRepo(int cardId)
        {
            return _context.Cards.Where(x => x.Id == cardId).Select(s => s.AccountId).FirstOrDefault();
        }

        public async Task<Card> GetCardIncludesAsync(string number)
        {
            return await _context.Cards.Where(x => x.IsDeleted == false && x.Number == number).Include(x => x.Account).ThenInclude(x => x.Currency).FirstOrDefaultAsync();
        }

        public bool ValidateIdentifier(string identifier)
        {
            if (_context.Cards.ToList().Any(c => c.Number.Substring(11, 4) == identifier))
            {
                return false;
            }
            return true;

        }

        public Card GetCardByName(string name)
        {
            return _context.Cards.Where(x => !x.IsDeleted && x.Name == name).FirstOrDefault();
        }
    }
}


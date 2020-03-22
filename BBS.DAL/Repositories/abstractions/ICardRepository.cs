using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface ICardRepository : IBaseRepository<Card>
    {
        public Task<Card> GetCardAsync(int id);
        public Task<Card> GetCardIncludesAsync(int id);
        public Task<Card> GetCardIncludesAsync(string number);
        public Task<List<Card>> GetCardsAsync();
        public Task<List<Card>> GetCardsIncludesAsync();
        public Task<List<Card>> GetCardsIncludesAsync(int userId);
        Task<IEnumerable<Card>> GetCardsByUserIdAsync(int userId);
        int GetSenderAccountIdRepo(int cardId);
        bool ValidateIdentifier(string identifier);
        Card GetCardByName(string name);
    }
}

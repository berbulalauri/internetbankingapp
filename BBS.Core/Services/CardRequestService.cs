using BBS.DAL.Repositories.abstractions;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
namespace BBS.Core.Services
{
    public class CardRequestService : ICardRequestService
    {
        private readonly ICardRequestRepository _cardRequestRepository;
        public CardRequestService(ICardRequestRepository cardRequestRepository)
        {
            _cardRequestRepository = cardRequestRepository;
        }

        public async Task Add(CardRequest cardRequest)
        {
            await _cardRequestRepository.InsertAsync(cardRequest);
        }

        public async Task Delete(int id)
        {
            await _cardRequestRepository.DeleteAsync(id);
        }

        public async Task<List<CardRequest>> GetAll()
        {
            return (await _cardRequestRepository.GetAllAsync()).ToList();
        }

        public async Task<List<CardRequest>> GetAllIncludes()
        {
            return (await _cardRequestRepository.GetAllIncludesAsync()).ToList();
        }

        public async Task<CardRequest> GetById(int? id)
        {
            return await _cardRequestRepository.GetByIdAsync(id);
        }

        public async Task<CardRequest> GetByIdIncludes(int id)
        {
            return await _cardRequestRepository.GetByIdIncludesAsync(id);
        }

        public async Task<CardRequest> GetByUserId(int? id)
        {
            return await _cardRequestRepository.GetByUserIdAsync(id);
        }

        public async Task Update(CardRequest cardRequest)
        {
            await _cardRequestRepository.UpdateAsync(cardRequest);
        }
    }
}

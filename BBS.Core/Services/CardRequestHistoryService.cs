using BBS.DAL.Repositories.abstractions;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class CardRequestHistoryService : ICardRequestHistoryService
    {
        private readonly ICardRequestHistoryRepository _cardRequestHistoryRepository;
        private readonly ICardRequestService _cardRequestService;


        public CardRequestHistoryService(ICardRequestHistoryRepository cardRequestHistoryRepository, ICardRequestService cardRequestService)
        {
            _cardRequestService = cardRequestService;
            _cardRequestHistoryRepository = cardRequestHistoryRepository;
        }

        public async Task Add(CardRequestHistory cardRequestHistory)
        {
            await _cardRequestHistoryRepository.InsertAsync(cardRequestHistory);
            await _cardRequestHistoryRepository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            await _cardRequestHistoryRepository.DeleteAsync(id);
        }

        public async Task<List<CardRequestHistory>> GetAll()
        {
            return (await _cardRequestHistoryRepository.GetAllAsync()).ToList();
        }

        public async Task<List<CardRequestHistory>> GetAllByRequestIdIncludes(int id)
        {
            return await _cardRequestHistoryRepository.GetAllByRequestIdIncludesAsync(id);
        }

        public Task<List<CardRequestHistory>> GetAllIncludes()
        {
            throw new NotImplementedException();
        }

        public async Task<CardRequestHistory> GetById(int? id)
        {
            return await _cardRequestHistoryRepository.GetByIdAsync(id);
        }

        public async Task Update(CardRequestHistory cardRequestHistory)
        {
            await _cardRequestHistoryRepository.UpdateAsync(cardRequestHistory);
        }
        public async Task MakeHistory(int userId)
        {
            var cardRequest = await _cardRequestService.GetByUserId(userId);
            CardRequestHistory cardRequestHistory = new CardRequestHistory
            {
                CardRequestId = cardRequest.Id,
                CreatedAt = cardRequest.CreatedAt,
                RequestStatus = cardRequest.CardRequestStatus
            };
            await Add(cardRequestHistory);
        }

    }
}

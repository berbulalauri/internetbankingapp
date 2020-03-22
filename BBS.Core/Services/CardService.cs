using BBS.Core.Helpers.CardHelper;
using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
	public class CardService : ICardService
	{


		ICardRepository _cardRepository;
		private ILogger _logger;
		private IAccountPropertyRepository _accountPropertyRepository;

		public CardService(ICardRepository cardRepository, IAccountPropertyRepository accountPropertyRepository, ILogger logger)
		{
			_cardRepository = cardRepository;
			_accountPropertyRepository = accountPropertyRepository;
			_logger = logger;
		}

		public async Task DeactivateCard(Card card)
		{
			_logger.Info($"Deactivated Card by name: {card.Name}");
			Card cardToDeactivate = await _cardRepository.GetByIdAsync(card.Id);
			cardToDeactivate.IsDeleted = true;
			await _cardRepository.UpdateAsync(cardToDeactivate);
			await _cardRepository.SaveAsync();
		}

		public async Task GenerateCard(Account account, string type)
		{
			_logger.Info($"Genereated Card by name: {account.Name}");
			string cardIdentifier = GenerationHelper.GenerateIdentifier();
			int timeSpan = PropertyHelper.ValidSpanYears;
			string initialStatus = PropertyHelper.InitialStatus;
			int cvc2 = GenerationHelper.GenerateCvc();

			if (!_cardRepository.ValidateIdentifier(cardIdentifier))
			{
				await GenerateCard(account, type);
			}

			string bankIdentifier = _accountPropertyRepository.GetPropertyModel().BankIdentificationNumber;
			string industryIdentifier = _accountPropertyRepository.GetPropertyModel().MajorIndustryIdentifier;
			string partialCardNumber = industryIdentifier + bankIdentifier + account.Number + cardIdentifier;

			string checksum = GenerationHelper.GenerateChecksum(partialCardNumber);

			string cardNumber = partialCardNumber + checksum;

			Card card = new Card() { Name = type, AccountId = account.Id, IsDefault = true, UserId = account.UserId, Number = cardNumber, RegDate = DateTime.Now, ExpDate = DateTime.Now.AddYears(timeSpan), Status = initialStatus, Cvc2 = cvc2 };

			await _cardRepository.InsertAsync(card);
			await _cardRepository.SaveAsync();

		}

		public async Task<Card> GetCardAsync(int id)
		{
			return await _cardRepository.GetCardIncludesAsync(id);
		}

		public async Task<Card> GetCardAsync(string number)
		{
			return await _cardRepository.GetCardIncludesAsync(number);
		}

		public async Task<List<Card>> GetCardsAsync(int userId)
		{
			return await _cardRepository.GetCardsIncludesAsync(userId);
		}

		public async Task<List<Card>> GetCardsAsync()
		{
			return await _cardRepository.GetCardsIncludesAsync();
		}

		public async Task<IEnumerable<Card>> GetCardsForPaymentAsync(int userId)
		{
			return await _cardRepository.GetCardsByUserIdAsync(userId);
		}


	}
}

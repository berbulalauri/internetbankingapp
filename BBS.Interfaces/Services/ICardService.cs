using BBS.Models.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
	public interface ICardService
	{
		public Task<Card> GetCardAsync(int id);
		public Task<Card> GetCardAsync(string number);
		public Task<List<Card>> GetCardsAsync(int userId);
		public Task<List<Card>> GetCardsAsync();
		public Task<IEnumerable<Card>> GetCardsForPaymentAsync(int userId);
		public Task GenerateCard(Account account, string type);
		Task DeactivateCard(Card card);
	}
}

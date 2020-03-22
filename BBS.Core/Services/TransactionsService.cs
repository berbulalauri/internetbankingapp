using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Constants;
using BBS.Models.CustomExceptions;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
	public class TransactionsService : ITransactionsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<User> _userManager;
		private readonly ILogger _logger;

		public TransactionsService(IUnitOfWork unit, UserManager<User> userManager, ILogger logger)
		{
			_unitOfWork = unit;
			_userManager = userManager;
			_logger = logger;
		}

		public async Task<Transactions> ServicePayment(Transactions transaction, ClaimsPrincipal currentUser)
		{
			User user = await _userManager.GetUserAsync(currentUser);
			Card card = await _unitOfWork.CardRepository.GetCardIncludesAsync(transaction.SenderCardId);

			if (transaction.Service == null)
			{
				transaction.Service = await _unitOfWork.ServiceRepository.GetByIdAsync(transaction.ServiceId);
			}

			if (card == null)
			{
				throw new ObjectNotFoundException();
			}
			else if (card.UserId != user.Id)
			{
				_logger.Warn($"User {user.Email} tried to use someone's card!");
				throw new CardOwnerException();
			}

			if (transaction.Amount <= 0 || transaction.Amount < transaction.Service.FixedFee)
			{
				throw new InvalidOperationException();
			}

			if(transaction.Amount > card.Account.Balance)
			{
				throw new BBS.Models.CustomExceptions.NotEnoughMoneyException();
			}

			await _unitOfWork.BeginTransactionAsync();

			try
			{
				transaction.SenderCard = card;
				transaction.SenderCard.Account.Balance -= transaction.Amount;
				transaction.SenderAccountId = transaction.SenderCard.AccountId;
				transaction.ReceiverAccountId = transaction.ReceiverAccount?.Id ?? transaction.ReceiverAccountId;
				transaction.ServiceId = transaction.Service?.Id??transaction.ServiceId;

				decimal totalFee = transaction.Amount * transaction.Service.PercentFee + transaction.Service.FixedFee;
				transaction.ReceiverAccount.Balance += transaction.Amount - totalFee;
				Account bank = (await _unitOfWork.AccountRepository.FindAllTrackingAsync(x => x.Name == BankStringConstants.BankAccountName)).First();
				bank.Balance += totalFee;

				transaction.SenderAccount = null;
				transaction.SenderCard = null;
				transaction.Service = null;

				await _unitOfWork.TransactionsRepository.InsertAsync(transaction);
				await _unitOfWork.AccountRepository.SaveAsync();

				await _unitOfWork.CommitAsync();
				return transaction;
			}
			catch(Exception e)
			{
				_logger.Error(e, "Error in transaction");
				await _unitOfWork.RollbackAsync();
				throw;
			}
		}

		public void Add(Transactions transactions)
		{
			_unitOfWork.TransactionsRepository.InsertAsync(transactions);
		}

		public async Task<List<Transactions>> TransactionsByUserId(int userId)
		{
			return await _unitOfWork.TransactionsRepository.TransactionsWithInclides(userId);
		}
	}
}

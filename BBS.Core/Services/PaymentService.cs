using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.CustomExceptions;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class PaymentService : IPaymentService
    {
        private const int AdminAccountId = 1;
        private readonly UserManager<User> _userManager;
        private readonly IServiceService _serviceService;
        protected IUnitOfWork _uniofWork;
        private readonly ILogger _logger;

        public PaymentService(IUnitOfWork uniofWork, UserManager<User> userManager, ILogger logger, IServiceService serviceService)
        {
            _userManager = userManager;
            _serviceService = serviceService;
            _uniofWork = uniofWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Service>> GetServicesById(int id)
        {
            return await _uniofWork.ServiceRepository.FindAllAsync(x => x.CategoryId == id);
        }
        public int GetSenderAccountId(int cardId)
        {
            return _uniofWork.CardRepository.GetSenderAccountIdRepo(cardId);
        }

        public int? GetReceiverAccountId(int serviceId)
        {
            return _uniofWork.ServiceRepository.GetReceiverAccountIdRepo(serviceId);
        }

        public async Task AddTransactionToHistory(Transactions transactions)
        {
            await _uniofWork.TransactionsRepository.InsertAsync(transactions);
            await _uniofWork.SaveChangesAsync();
        }

        public string GetReceiverNameService(int serviceId)
        {
            return _uniofWork.ServiceRepository.GetReceiverNameRepo(serviceId);
        }
        public async Task PaymentForService(Transactions transactions)
        {
            
            transactions.SenderAccountId = GetSenderAccountId(transactions.SenderCardId);
            transactions.ReceiverAccountId = GetReceiverAccountId(transactions.ServiceId)??transactions.ReceiverAccountId;
            var enoughMoney = await HasEnoughMoney(transactions.SenderAccountId, transactions.Amount, transactions.ServiceId);
            if (!enoughMoney)
            {
                throw new BBS.Models.CustomExceptions.NotEnoughMoneyException();
            }

            await TransferMoneyToService(transactions.SenderAccountId,
                                                        transactions.ReceiverAccountId,
                                                        transactions.Amount, 
                                                        transactions.ServiceId);
            transactions.Amount += await CalculateComission(transactions.Amount, transactions.ServiceId);
            await AddTransactionToHistory(transactions);
        }

        public async Task GeneralTransfer(Transactions transaction, ClaimsPrincipal currentUser)
        {
            User user = await _userManager.GetUserAsync(currentUser);
            Card card = await _uniofWork.CardRepository.GetCardIncludesAsync(transaction.SenderCardId);
            transaction.Service = await _serviceService.GetServiceAsync(transaction.ServiceId);
            transaction.SenderAccountId = card.AccountId;

            if (card == null)
            {
                throw new ObjectNotFoundException();
            }
            else if (card.UserId != user.Id)
            {
                _logger.Warn($"User {user.Email} tried to use someone's card!");
                throw new CardOwnerException();
            }
            else if (transaction.Amount <= 0 || transaction.Amount <= transaction.Service.FixedFee)
            {
                throw new InvalidOperationException();
            }
            else if (!await HasEnoughMoney(transaction.SenderAccountId, transaction.Amount, transaction.ServiceId))
            {
                throw new BBS.Models.CustomExceptions.NotEnoughMoneyException();
            }
            else
            {
                await TransferMoneyToService(transaction.SenderAccountId,
                                                        transaction.ReceiverAccountId,
                                                        transaction.Amount,
                                                        transaction.ServiceId);
                transaction.Amount += await CalculateComission(transaction.Amount, transaction.ServiceId);
                await AddTransactionToHistory(transaction);
            }
        }

        public async Task TransferMoneyToService(int senderAccountId, int receiverAccountId, decimal amount, int serviceId)
        {
            try
            {
                await _uniofWork.BeginTransactionAsync();
                await PayCommision(amount, serviceId);
                await WithdrawMoneyFromUserAccount(senderAccountId, amount, await CalculateComission(amount, serviceId));
                await DepositMoneyToService(receiverAccountId, amount);
                await _uniofWork.SaveChangesAsync();
                await _uniofWork.CommitAsync();
            }
            catch (Exception)
            {
                await _uniofWork.RollbackAsync();
            }
        }

        public async  Task<bool> HasEnoughMoney(int senderAccountId, decimal amount, int serviceId)
        {
            decimal adminCommision = await CalculateComission(amount, serviceId);
            var userBalanceTask = await _uniofWork.AccountRepository.GetByIdAsync(senderAccountId);
            var userBalance = userBalanceTask.Balance;
            return (userBalance + adminCommision >= amount);
           
        }
        private async Task PayCommision(decimal amount, int serviceId)
        {
            var result = await _uniofWork.AccountRepository.GetByIdAsync(AdminAccountId);
            result.Balance += await CalculateComission(amount, serviceId);
        }

        private async Task<decimal> CalculateComission(decimal amount, int serviceId)
        {
            var service = await _serviceService.GetServiceAsync(serviceId);
            var result = ((amount / 100) * service.PercentFee) + service.FixedFee;

            return result;
        }

        private async Task DepositMoneyToService(int receiverAccountId, decimal amount)
        {
            var result = await _uniofWork.AccountRepository.GetByIdAsync(receiverAccountId);
            result.Balance += amount;
        }

        private async Task WithdrawMoneyFromUserAccount(int senderAccountId, decimal amount, decimal adminCommision)
        {
            var result = await _uniofWork.AccountRepository.GetByIdAsync(senderAccountId);
            result.Balance -= amount+ adminCommision;
        }
    }
}

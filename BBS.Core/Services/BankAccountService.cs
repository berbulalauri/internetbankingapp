using BBS.Core.Helpers.AccountHelper;
using BBS.Core.Helpers.CardHelper;
using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly IAccountRepository _accountRepository;

        private readonly ILogger _logger;

        private readonly ICardService _cardService;

        public BankAccountService(IAccountRepository accountRepository, ICardService cardService, ILogger logger)
        {
            _accountRepository = accountRepository;
            _cardService = cardService;
            _logger = logger;
        }

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetByNameAsync(string name)
        {
            return (await _accountRepository.GetAllTrackingAsync()).FirstOrDefault(x => x.Name == name);
        }

        public async Task DeactivateAccount(Account account)
        {
            _logger.Info($"Deactivated account by name: {account.Name}");
            Account accountToDeactivate = await _accountRepository.GetByIdAsync(account.Id);
            accountToDeactivate.IsDeleted = true;
            await _accountRepository.UpdateAsync(accountToDeactivate);
            await _accountRepository.SaveAsync();
        }

        public void DeactivateAccount()
        {
            throw new NotImplementedException();
        }

        public async Task DeactivateCard(Card card)
        {
            await _cardService.DeactivateCard(card);
        }

        public void DeactivateCard()
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GenerateBankAccount(User user, Currency currency)
        {
            _logger.Info($"Generetated user by email: {user.Email} with Currency  name: {currency.Name}");
            string accountIdentifier = Helpers.AccountHelper.GenerationHelper.GenerateAccountIdentifierNumber();
            if (!_accountRepository.ValidateAccountIdentifier(accountIdentifier))
            {
                return await GenerateBankAccount(user, currency);
            }
            string accountName = Helpers.AccountHelper.GenerationHelper.GetAccountName(user.FirstName);

            Account account = new Account() { Name = accountName, Number = accountIdentifier, CurrencyId = currency.Id, RegDate = DateTime.Now, UserId = user.Id, Balance = Helpers.AccountHelper.PropertyHelper.InitialBalance, Status = Helpers.AccountHelper.PropertyHelper.InitialStatus };

            await _accountRepository.InsertAsync(account);
            await _accountRepository.SaveAsync();

            Account createdAccount = _accountRepository.GetByIdentifier(accountIdentifier);

            await IssueCard(createdAccount, CardTypes.typeOrdinary);

            return account;
        }

        public async Task<List<Account>> GetUserAccounts(int userId)
        {
            return await _accountRepository.GetUserAccountsTracking(userId);
        }

        public async Task IssueCard(Account createdAccount, string type)
        {
            await _cardService.GenerateCard(createdAccount, type);
        }

    }
}

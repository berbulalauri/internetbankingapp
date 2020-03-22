using BBS.Core.Helpers;
using BBS.DAL.Repositories;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Constants;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class DepositService : IDepositService
    {
        private readonly IDepositTypeRepository _depositTypeRepo;
        private readonly IDepositRepository _DepositRepository;
        private readonly ITransactionsRepository _transactionsService;
        private readonly ICardService _cardService;
        private readonly IAccountRepository _accountRepository;
        private ILogger _logger;
        private readonly IDepositTermService _depositTermService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServiceRepository _serviceRepository;


        public DepositService(IDepositTypeRepository typeRepository,
            IDepositRepository repository,
            ILogger logger,
            ITransactionsRepository transactionsService, ICardService cardService, IAccountRepository accountRepository, IDepositTermService depositTermService,
            IUnitOfWork unitOfWork,
            IServiceRepository serviceRepository)
        {
            _depositTypeRepo = typeRepository;
            _DepositRepository = repository;
            _logger = logger;
            _transactionsService = transactionsService;
            _cardService = cardService;
            _accountRepository = accountRepository;
            _depositTermService = depositTermService;
            _unitOfWork = unitOfWork;
            _serviceRepository = serviceRepository;
        }

        public async Task<IEnumerable<DepositType>> GetAllTypes()
        {
            return await _depositTypeRepo.GetAllWithCurrency();
        }

        public async Task<IEnumerable<DepositType>> GetAllWithTerm()
        {
            return await _depositTypeRepo.GetAllWithTerm();
        }

        public async Task<List<DepositType>> GetWithFilter(decimal amountString, int currencyString, int termString)
        {
            return await _depositTypeRepo.GetWithFilter(amountString, currencyString, termString);
        }

        public async Task<DepositType> GetType(int id)
        {
            return await _depositTypeRepo.GetWithCurrency(id);
        }

        public async Task CreateDepositType(DepositType depositType)
        {
            await _depositTypeRepo.InsertAsync(depositType);
            await _depositTypeRepo.SaveAsync();
            _logger.Info($"Created by Name: {depositType.Name}");
        }

        public async Task Delete(int id)
        {
            await _depositTypeRepo.DeleteAsync(id);
            await _depositTypeRepo.SaveAsync();
            _logger.Info($"Deleted by id: {id.ToString()}");
        }

        public async Task<DepositType> GetDepositTypeService(int id)
        {
            var getDepositTypeInfo = await _depositTypeRepo.GetDepositTypeRepository(id);
            return getDepositTypeInfo;
        }

        public async Task UpdateDepositTypeService(DepositType depositType)
        {
            await _depositTypeRepo.UpdateAsync(depositType);
            await _depositTypeRepo.SaveAsync();
        }

        public async Task Add(Deposit deposit)
        {
            await _DepositRepository.Add(deposit);
            await _depositTypeRepo.SaveAsync();
        }

        public async Task<bool> OpenDepositAsync(Deposit deposit, int id, Account account)
        {
            await _unitOfWork.BeginTransactionAsync();
            var accountDeposit = await _accountRepository.GetByIdAsync(deposit.AccountForInterestId);
            if (deposit.DepositAmount > account.Balance)
            {
                throw new NotEnoughMoneyException();
            }
            try
            {
                account.Balance -= deposit.DepositAmount;
                accountDeposit.Balance += deposit.DepositAmount;

                deposit.Status = DepositConstants.Active;
                deposit.UserId = id;
                StringBuilder stringBuilder = new StringBuilder();

                var terms = await _depositTermService.GetAllWithTerm();

                var stringMonths = new string(terms
                     .FirstOrDefault(x => x.Id == deposit.DepositTermId)
                     .Name.ToCharArray()
                    .Where(x => char.IsDigit(x))
                    .Select(x => x).ToArray());

                int.TryParse(stringMonths, out int month);

                deposit.TermOfDeposit = DateTime.Now.AddMonths(month);

                var cards = await _cardService.GetCardsAsync();
                var senderCardId = cards.FirstOrDefault(x => account.Id == x.AccountId).Id;

                var accounts = await _accountRepository.GetAllAsync();
                var services = await _serviceRepository.GetAllAsync();


                var bankAccountId = accounts.FirstOrDefault(x => x.Name == DepositConstants.BankAccount).Id;
                var bankServiceId = services.FirstOrDefault(x => x.Name == DepositConstants.BankDepositService).Id;
                var bankCardId = cards.FirstOrDefault(x => x.Name == DepositConstants.BankCard).Id;

                var userToBankTransaction = new Transactions
                {
                    PersonalAccountId = "",
                    SenderCardId = senderCardId,
                    ReceiverAccountId = bankAccountId,
                    SenderAccountId = account.Id,
                    ServiceId = bankServiceId,
                    Description = $"Moved money from sorce acc to bank acc",
                    Amount = deposit.DepositAmount,
                    Date = DateTime.Now,
                    IsDeleted = false,
                };

                var bankToDepositTransaction = new Transactions
                {
                    PersonalAccountId = "",
                    SenderCardId = bankCardId,
                    ReceiverAccountId = deposit.AccountForInterestId,
                    SenderAccountId = bankAccountId,
                    ServiceId = bankServiceId,
                    Description = $"Moved money from bank acc to deposit acc",
                    Amount = deposit.DepositAmount,
                    Date = DateTime.Now,
                    IsDeleted = false,
                };

                await _transactionsService.InsertAsync(userToBankTransaction);
                await _transactionsService.InsertAsync(bankToDepositTransaction);
                await Add(deposit);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                return false;
            }

            return true;
        }

        public async Task AddDepositPercents()
        {
            var deposits = await _DepositRepository.GetAllWithTrackingAsync();
            var cards = await _cardService.GetCardsAsync();
            var accounts = await _accountRepository.GetAllAsync();
            var services = await _serviceRepository.GetAllAsync();


            var bankAccountId = accounts.FirstOrDefault(x => x.Name == DepositConstants.BankAccount).Id;
            var bankServiceId = services.FirstOrDefault(x => x.Name == DepositConstants.BankDepositService).Id;
            var bankCardId = cards.FirstOrDefault(x => x.Name == DepositConstants.BankCard).Id;
            foreach (var deposit in deposits)
            {
                if (deposit.Status == DepositConstants.Active)
                {
                    var depositType = await _depositTypeRepo.GetByIdAsync(deposit.DepositTypeId);
                    var percent = depositType.AnnualRate;

                    var account = await _accountRepository.GetByIdAsync(deposit.AccountForInterestId);
                    var bankAccount = await _accountRepository.GetByIdAsync(bankAccountId);

                    var depositPercent = ((deposit.DepositAmount * percent) / DepositConstants.Percent100);
                    bankAccount.Balance -= depositPercent;
                    account.Balance += depositPercent;


                    var transaction = new Transactions
                    {
                        PersonalAccountId = "",
                        SenderCardId = bankCardId,
                        ReceiverAccountId = deposit.AccountForInterestId,
                        SenderAccountId = bankAccountId,
                        ServiceId = bankServiceId,
                        Description = $"add deposit percent",
                        Amount = depositPercent,
                        Date = DateTime.Now,
                        IsDeleted = false,
                    };

                    await _accountRepository.UpdateAsync(account);
                    await _accountRepository.UpdateAsync(bankAccount);
                    await _transactionsService.InsertAsync(transaction);


                    if (deposit.TermOfDeposit <= DateTime.Now)
                    {
                        deposit.Status = DepositConstants.Done;
                    }
                }
            }
        }

        public void Dispose()
        {

        }
        public async Task<List<DepositType>> Filter(string sortOrder, string columnName, string searchString)
        {
            IQueryable<DepositType> depositTypes = await _depositTypeRepo.GetDepositQuerry();

            switch (sortOrder)
            {
                case Order.Name:
                    depositTypes = depositTypes.OrderBy(d => d.Name);
                    break;
                case OrderDesc.Name:
                    depositTypes = depositTypes.OrderByDescending(d => d.Name);
                    break;
                case Order.Description:
                    depositTypes = depositTypes.OrderBy(d => d.Description);
                    break;
                case OrderDesc.Description:
                    depositTypes = depositTypes.OrderByDescending(d => d.Description);
                    break;
                case Order.BenefitsDescription:
                    depositTypes = depositTypes.OrderBy(d => d.BenefitsDescription);
                    break;
                case OrderDesc.BenefitsDescription:
                    depositTypes = depositTypes.OrderByDescending(d => d.BenefitsDescription);
                    break;
                case Order.AnnualRate:
                    depositTypes = depositTypes.OrderBy(d => d.AnnualRate);
                    break;
                case OrderDesc.AnnualRate:
                    depositTypes = depositTypes.OrderByDescending(d => d.AnnualRate);
                    break;
                case Order.BonusRate:
                    depositTypes = depositTypes.OrderBy(d => d.BonusRate);
                    break;
                case OrderDesc.BonusRate:
                    depositTypes = depositTypes.OrderByDescending(d => d.BonusRate);
                    break;
                case Order.MinimumDepositAmount:
                    depositTypes = depositTypes.OrderBy(d => d.MinimumDepositAmount);
                    break;
                case OrderDesc.MinimumDepositAmount:
                    depositTypes = depositTypes.OrderByDescending(d => d.MinimumDepositAmount);
                    break;
                case Order.MinimumReplenishmentAmount:
                    depositTypes = depositTypes.OrderBy(d => d.MinimumReplenishmentAmount);
                    break;
                case OrderDesc.MinimumReplenishmentAmount:
                    depositTypes = depositTypes.OrderByDescending(d => d.MinimumReplenishmentAmount);
                    break;
                case Order.MaximumDepositAmount:
                    depositTypes = depositTypes.OrderBy(d => d.MaximumDepositAmount);
                    break;
                case OrderDesc.MaximumDepositAmount:
                    depositTypes = depositTypes.OrderByDescending(d => d.MaximumDepositAmount);
                    break;
                case Order.Currency:
                    depositTypes = depositTypes.OrderBy(d => d.Currency);
                    break;
                case OrderDesc.Currency:
                    depositTypes = depositTypes.OrderByDescending(d => d.Currency);
                    break;
                case Order.InterestPaymentType:
                    depositTypes = depositTypes.OrderBy(d => d.InterestPaymentType);
                    break;
                case OrderDesc.InterestPaymentType:
                    depositTypes = depositTypes.OrderByDescending(d => d.InterestPaymentType);
                    break;
                case Order.DepositTerm:
                    depositTypes = depositTypes.OrderBy(d => d.DepositTerm);
                    break;
                case OrderDesc.DepositTerm:
                    depositTypes = depositTypes.OrderByDescending(d => d.DepositTerm);
                    break;
                default:
                    depositTypes = depositTypes.OrderBy(d => d.Name);
                    break;
            }

            List<DepositType> filetered = new List<DepositType>();

            foreach (var item in depositTypes)
            {
                if (item.GetType().GetProperty(columnName).GetValue(item).ToString().Contains(searchString ?? "", StringComparison.OrdinalIgnoreCase))
                {
                    filetered.Add(item);
                }
            }

            return filetered;
        }
    }
}

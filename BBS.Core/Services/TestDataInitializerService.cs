using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class TestDataInitializerService : ITestDataInitializerService
    {
        private readonly ILogger _logger;
        private readonly IDepositTypeRepository _depositTypeRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IServiceCategoryRepository _serviceCategoryRepository;
        private readonly ILoanTypeRepository _loanTypeRepository;
        private readonly IServiceRepository _serviceRepository;

        public TestDataInitializerService(IDepositTypeRepository depositTypeRepository,
            IServiceCategoryRepository servCatRepo, IServiceRepository servRepo,
            ICardRepository cardRepository, IAccountRepository accountRepository,
            ILoanTypeRepository loanTypeRepository, ILogger logger)
        {
            _depositTypeRepository = depositTypeRepository;
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _loanTypeRepository = loanTypeRepository;
            _serviceCategoryRepository = servCatRepo;
            _serviceRepository = servRepo;
            _logger = logger;
        }

        public async Task CreateAccount(Account account)
        {
            await _accountRepository.InsertAsync(account);
            await _accountRepository.SaveAsync();
        }

        public async Task CreateCard(Card card)
        {
            await _cardRepository.InsertAsync(card);
            await _cardRepository.SaveAsync();
        }

        public async Task CreateDepositType(DepositType depositType)
        {
            await _depositTypeRepository.InsertAsync(depositType);
            await _depositTypeRepository.SaveAsync();
        }

        public async Task CreateLoanType(LoanType loanType)
        {
            await _loanTypeRepository.InsertAsync(loanType);
            await _loanTypeRepository.SaveAsync();
        }

        public async Task CreateService(Service service)
        {
            await _serviceRepository.InsertAsync(service);
            await _serviceRepository.SaveAsync();
        }

        public async Task CreateServiceCategory(ServiceCategory service)
        {
            await _serviceCategoryRepository.InsertAsync(service);
            await _serviceCategoryRepository.SaveAsync();
        }
    }
}

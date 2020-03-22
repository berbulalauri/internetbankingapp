using BBS.DAL.Database;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private BankDbContext _context;
        private IDbContextTransaction _transaction;

        //I implemented lazy loading of repositories 
        //this repositories will not be created unless unitofWork explicitly call's  them

        private Lazy<IAccountRepository> _accountRepository;
        private Lazy<ICardRepository> _cardRepository;
        private Lazy<ICityRepository> _cityRepository;
        private Lazy<ICurrencyRepository> _currencyRepository;
        private Lazy<IDepositRepository> _depositRepository;
        private Lazy<IDepositTypeRepository> _depositTypeRepository;
        private Lazy<IEmploymentRepository> _employmentRepository;
        private Lazy<IEmploymentTypeRepository> _employmentTypeRepository;
        private Lazy<IInterestPaymentTypeRepository> _interestPaymentTypeRepository;
        private Lazy<ILoanRepository> _loanRepository;
        private Lazy<ILoanRequestRepository> _loanRequestRepository;
        private Lazy<ILoanTypeRepository> _loanTypeRepository;
        private Lazy<IQuestionRepository> _questionRepository;
        private Lazy<IServiceCategoryRepository> _serviceCategoryRepository;
        private Lazy<IServiceRepository> _serviceRepository;
        private Lazy<ITransactionsRepository> _transactionsRepository;
        private Lazy<IAccountPropertyRepository> _accountPropertyRepository;
        public UnitOfWork(
             BankDbContext context
            , IAccountRepository accountRepository
            , ICardRepository cardRepository
            , ICityRepository cityRepository
            , ICurrencyRepository currencyRepository
            , IDepositRepository depositRepository
            , IDepositTypeRepository depositTypeRepository
            , IEmploymentRepository employmentRepository
            , IEmploymentTypeRepository employmentTypeRepository
            , IInterestPaymentTypeRepository interestPaymentTypeRepository
            , ILoanRepository loanRepository
            , ILoanRequestRepository loanRequestRepository
            , ILoanTypeRepository loanTypeRepository
            , IQuestionRepository questionRepository
            , IServiceCategoryRepository serviceCategoryRepository
            , IServiceRepository serviceRepository
            , ITransactionsRepository transactionsRepository
            , IAccountPropertyRepository accountPropertyRepository
            )
        {
            _context = context;
            _accountRepository = new Lazy<IAccountRepository>(accountRepository);
            _cardRepository = new Lazy<ICardRepository>(cardRepository);
            _cityRepository = new Lazy<ICityRepository>(cityRepository);
            _currencyRepository = new Lazy<ICurrencyRepository>(currencyRepository);
            _depositRepository = new Lazy<IDepositRepository>(depositRepository);
            _depositTypeRepository= new Lazy<IDepositTypeRepository>(depositTypeRepository);
            _employmentRepository = new Lazy<IEmploymentRepository>(employmentRepository);
            _employmentTypeRepository = new Lazy<IEmploymentTypeRepository>(employmentTypeRepository);
            _interestPaymentTypeRepository = new Lazy<IInterestPaymentTypeRepository>(interestPaymentTypeRepository);
            _loanRepository = new Lazy<ILoanRepository>(loanRepository);
            _loanRequestRepository = new Lazy<ILoanRequestRepository>(loanRequestRepository);
            _loanTypeRepository = new Lazy<ILoanTypeRepository>(loanTypeRepository);
            _questionRepository = new Lazy<IQuestionRepository>(questionRepository);
            _serviceCategoryRepository = new Lazy<IServiceCategoryRepository>(serviceCategoryRepository);
            _serviceRepository = new Lazy<IServiceRepository>(serviceRepository);
            _transactionsRepository = new Lazy<ITransactionsRepository>(transactionsRepository);
            _accountPropertyRepository = new Lazy<IAccountPropertyRepository>(accountPropertyRepository);
        }

        public IAccountRepository AccountRepository => _accountRepository?.Value;
        public ICardRepository CardRepository => _cardRepository?.Value;
        public ICityRepository CityRepository => _cityRepository?.Value;
        public ICurrencyRepository CurrencyRepository => _currencyRepository?.Value;
        public IDepositRepository DepositRepository => _depositRepository?.Value;
        public IEmploymentRepository EmploymentRepository => _employmentRepository?.Value;
        public IEmploymentTypeRepository EmploymentTypeRepository => _employmentTypeRepository?.Value;
        public IInterestPaymentTypeRepository InterestPaymentTypeRepository => _interestPaymentTypeRepository?.Value;
        public ILoanRepository LoanRepository => _loanRepository?.Value;
        public ILoanRequestRepository LoanRequestRepository => _loanRequestRepository?.Value;
        public ILoanTypeRepository LoanTypeRepository => _loanTypeRepository?.Value;
        public IQuestionRepository QuestionRepository => _questionRepository?.Value;
        public IServiceCategoryRepository ServiceCategoryRepository => _serviceCategoryRepository?.Value;
        public IServiceRepository ServiceRepository => _serviceRepository?.Value;
        public ITransactionsRepository TransactionsRepository => _transactionsRepository?.Value;
        public IDepositTypeRepository DepositTypeRepository => _depositTypeRepository?.Value;
        public IAccountPropertyRepository AccountPropertyRepository => _accountPropertyRepository?.Value;

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

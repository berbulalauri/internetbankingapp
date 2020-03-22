using System.Threading.Tasks;

namespace BBS.DAL.Repositories.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAccountRepository AccountRepository { get; }
        ICardRepository CardRepository { get; }
        ICityRepository CityRepository { get; }
        ICurrencyRepository CurrencyRepository { get; }
        IDepositRepository DepositRepository { get; }
        IDepositTypeRepository DepositTypeRepository { get; }
        IEmploymentRepository EmploymentRepository { get; }
        IEmploymentTypeRepository EmploymentTypeRepository { get; }
        IInterestPaymentTypeRepository InterestPaymentTypeRepository { get; }
        ILoanRepository LoanRepository { get; }
        ILoanRequestRepository LoanRequestRepository { get; }
        ILoanTypeRepository LoanTypeRepository { get; }
        IQuestionRepository QuestionRepository { get; }
        IServiceCategoryRepository ServiceCategoryRepository { get; }
        IServiceRepository ServiceRepository { get; }
        ITransactionsRepository TransactionsRepository { get; }
        IAccountPropertyRepository AccountPropertyRepository { get; }

        Task BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
        Task SaveChangesAsync();
    }
}
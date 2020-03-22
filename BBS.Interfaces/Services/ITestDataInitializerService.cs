using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ITestDataInitializerService
    {
        Task CreateDepositType(DepositType depositType);
        Task CreateLoanType(LoanType loanType);
        Task CreateAccount(Account account);
        Task CreateCard(Card card);
        Task CreateService(Service service);
        Task CreateServiceCategory(ServiceCategory service);
    }
}

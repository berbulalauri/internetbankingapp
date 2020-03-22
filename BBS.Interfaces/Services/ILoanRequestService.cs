using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ILoanRequestService
    {
        Task<IEnumerable<LoanRequest>> GetAllUsersLoan(int userId);
        Task<IEnumerable<LoanRequest>> GetAllAcceptedByAdmin();
        Task<IEnumerable<LoanRequest>> GetAllOnReviewByAdmin();
        Task<IEnumerable<LoanRequest>> GetAllRejectedByAdmin();
        Task<LoanRequest> GetById(int id);
        Task CreateLoanRequest(LoanRequest loanRequest, LoanType loanType, int userId, int employmentId);
        Task DeclineLoanRequest(LoanRequest loanRequest, string comment);
    }
}

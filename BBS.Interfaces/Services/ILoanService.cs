using BBS.Models.Models;
using System;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ILoanService : IDisposable
    {
        Task CreateLoan(int loanRequestId);
        Task PayLoans();
    }
}

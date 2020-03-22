using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ILoanTypeService
    {
        public Task<IEnumerable<LoanType>> GetLoanTypes();
        public Task<LoanType> GetLoanType(int Id);
        Task AddLoan(LoanType loanType);
        Task UpdateLoanType(LoanType loanType);
        Task DeleteLoanType(int Id);
        Task<LoanType> LoanTypeDetailsService(int id);


    }
}

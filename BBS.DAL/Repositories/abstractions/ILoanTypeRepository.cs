using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface ILoanTypeRepository : IBaseRepository<LoanType>
    {
        Task<List<LoanType>> GetAllLoanTypeWithCurrencyAsync();
        Task<LoanType> GetLoanTypeWithCurrencyByIdAsync(int id);
        LoanType GetByIdAsync(int id);
    }
}


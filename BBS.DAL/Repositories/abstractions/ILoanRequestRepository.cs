using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface ILoanRequestRepository : IBaseRepository<LoanRequest>
    {
        Task<IEnumerable<LoanRequest>> GetAllWithFullInfo();
        Task<LoanRequest> GetByIdWithAllInfoAsync(int id);
    }
}


using BBS.Models.Models;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface ILoanRepository : IBaseRepository<Loan>
    {
        public Task<List<Loan>> GetAllLoans();
        Task<int> GetTotalLoanCount();
        Task<decimal> GetTotalLoanAmount();
        Task<IEnumerable<Loan>> GetAllWithTrackingAsync();
    }
}

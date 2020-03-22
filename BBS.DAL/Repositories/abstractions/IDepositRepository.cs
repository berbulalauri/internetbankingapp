using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface IDepositRepository:IBaseRepository<Deposit>
    {
        Task Add(Deposit deposit);
        Task<List<Deposit>> GetAllWithTrackingAsync();
        Task<int> GetTotalDepositCount();
        Task<decimal> GetTotalDepositAmount();
    }
}

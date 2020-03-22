using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface IDepositTypeRepository : IBaseRepository<DepositType>
    {
        public Task<List<DepositType>> GetAllWithCurrency();
        public Task<DepositType> GetWithCurrency(int Id);
        public Task<List<DepositType>> GetAllWithTerm();
        public Task<List<DepositType>> GetWithFilter(decimal amountString, int currencyString, int termString);
        public Task<DepositType> GetDepositTypeRepository(int Id);
        public Task<IQueryable<DepositType>> GetDepositQuerry();
    }
}
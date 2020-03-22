using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface ITransactionsRepository : IBaseRepository<Transactions>
    {
        public Task<List<Transactions>> TransactionsWithInclides();
        public Task<List<Transactions>> TransactionsWithInclides(int userId);
    }
}


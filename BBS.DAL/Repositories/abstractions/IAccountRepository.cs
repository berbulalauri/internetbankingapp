using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public interface IAccountRepository : IBaseRepository<Account>
    {
        void Add(Account account);
        Task<List<Account>> GetUserAccounts(int userId);
        Task<List<Account>> GetUserAccountsTracking(int userId);
        Task<List<Account>> GetAllTrackingAsync();
        Task<List<Account>> FindAllTrackingAsync(Expression<Func<Account, bool>> predicate);
        Task<Account> GetByIdTrackingAsync(int id);

        bool ValidateAccountIdentifier(string identifier);
        Account GetByIdentifier(string identifier);
        Account GetByName(string name);
    }
}

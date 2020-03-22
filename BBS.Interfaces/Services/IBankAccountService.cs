using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IBankAccountService
    {
        void DeactivateCard();
        void DeactivateAccount();
        void Add(Account account);
        Task<List<Account>> GetUserAccounts(int userId);
        Task<Account> GenerateBankAccount(User user,Currency currency);
        Task IssueCard(Account account, string type);
        Task DeactivateCard(Card card);
        Task DeactivateAccount(Account account);
        Task<Account> GetByNameAsync(string name);
    }
}

using BBS.DAL.Database;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories
{
    public class AccountRepository:BaseRepository<Account>, IAccountRepository
    {
        public AccountRepository(BankDbContext ctx):base(ctx)
        {
        }

        public Account GetByIdentifier(string identifier)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.Number == identifier);
        }

        public bool ValidateAccountIdentifier(string identifier)
        {
            if (_context.Accounts.AsNoTracking().ToList().Any(acc => acc.Number == identifier))
            {
                return false;
            }
            return true;
        }

        public async Task<List<Account>> FindAllTrackingAsync(Expression<Func<Account, bool>> predicate)
        {
            return await _context.Accounts.Where(x=>x.IsDeleted==false).Where(predicate).ToListAsync();
        }

        public async Task<List<Account>> GetAllTrackingAsync()
        {
            return await _context.Accounts.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Account> GetByIdTrackingAsync(int id)
        {
            return await _context.Accounts.Where(x => x.IsDeleted == false&& x.Id==id).FirstOrDefaultAsync();
        }


        public async Task<List<Account>> GetUserAccountsTracking(int userId)
        {
            return await _context.Accounts.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<List<Account>> GetUserAccounts(int userId)
        {
           return await _context.Accounts.Where(x => x.UserId == userId).AsNoTracking().ToListAsync();
        }

        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public Account GetByName(string name)
        {
            return _context.Accounts.Where(x => !x.IsDeleted && x.Name == name).FirstOrDefault();
        }
    }
}

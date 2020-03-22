using BBS.DAL.Database;
using BBS.DAL.Repositories.abstractions;
using BBS.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.concrete
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(BankDbContext ctx) : base(ctx)
        {
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Include(u => u.Question).FirstOrDefaultAsync(user => user.Email == email);
        }

        public async Task<User> GetUserById(int id)
        {
            var users = await _context.Users.Include(u => u.Question).Include(u => u.Accounts).ToListAsync();
            User user = users.Find(u => u.Id == id);
            return user;
        }

        public async Task<User> GetUserByPassportId(string passportId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.PassportId == passportId);
        }

        public async Task<int> GetExistingUserQuantity()
        {
            var totalUserCount = await _context.Users.Where(x => x.IsDeleted == false).Select(x => x.Id).ToListAsync();
            var result = totalUserCount.Count();
            return result;
        }

        public async Task<List<User>> GetUserListWithTransactions()
        {
            var getUserListAsync = await _context.Transactions.Include(d => d.SenderAccount.User.Question).Include(d => d.SenderAccount.User).ThenInclude(d => d.City).ToListAsync();
            var returnUserList = getUserListAsync.ConvertAll(x => x.SenderAccount.User).Distinct().ToList();
            return returnUserList;
        }

        public new async Task UpdateAsync(User user)
        {
            User userToUpdate = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            userToUpdate.PassportId = user.PassportId;
            userToUpdate.Phone = user.Phone;
            userToUpdate.Email = user.Email;
            userToUpdate.DefaultAccountId = user.DefaultAccountId;
            userToUpdate.AllowPhoneForLogin = user.AllowPhoneForLogin;
            userToUpdate.QuestionId = user.QuestionId;
            userToUpdate.Answer = user.Answer;

            await _context.SaveChangesAsync();
        }
    }
}

using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.DAL.Repositories.abstractions
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetUserById(int id);
        new Task UpdateAsync(User user);
        Task<User> GetUserByPassportId(string passportId);
        Task<List<User>> GetUserListWithTransactions();
        Task<int> GetExistingUserQuantity();
        Task<User> GetByEmail(string email);
    }
}

using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<bool> UserExists(int id);
        Task Update(User user);
        Task<User> CreateUser(User user, string password);
        Task DeleteUser(string username);
        Task<User> GetUserByEmail(string email);
        Task AddRole(User user, string role);
        Task AddClaim(User createdUser, Claim claim);
        Task<List<User>> GetUserListWhoMadeTransaction();
        Task GenerateCard(User createdUser);
        Task<IEnumerable<City>> GetUserCities();
        Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword);
        Task<User> GetUserByPhone(string phoneOrEmail);
        Task<IEnumerable<Account>> GetUserAccounts(int id);
        Task<Question> GetUserQuestion(int id);
        Task<User> GetCurrentUser(ClaimsPrincipal cp);
        Task<User> GetUserByPassportId(string passportId);
        Task<int> GetTotalUserCount();
        Task ResetPassword(User user, string password);
        public Task<User> GetByEmail(string email);
    }
}

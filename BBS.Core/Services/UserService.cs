using BBS.DAL.Repositories;
using BBS.DAL.Repositories.abstractions;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BBS.Core.Services
{
    public class UserService : IUserService
    {

        private UserManager<User> _userManager;
        private RoleManager<IdentityRole<int>> _rolesManager;
        private IBankAccountService _bankAccountService;
        private ICurrencyService _currencyService;
        private ICardService _cardService;
        private ICityRepository _cityRepository;
        private IUserRepository _userRepository;
        private IQuestionRepository _questionRepository;
        private IAccountRepository _accountRepository;
        private ILogger _logger;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> rolesManager,
            IBankAccountService bankAccountService,
            ICurrencyService currencyService,
            ICardService cardService,
            ICityRepository cityRepository,
            IUserRepository userRepository,
            IQuestionRepository questionRepository,
            IAccountRepository accountRepository,
            ILogger logger)
        {
            _userManager = userManager;
            _rolesManager = rolesManager;
            _bankAccountService = bankAccountService;
            _currencyService = currencyService;
            _cardService = cardService;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
            _questionRepository = questionRepository;
            _accountRepository = accountRepository;
            _logger = logger;
        }


        public async Task Update(string id, string username, string email, string phonenumber, string password)
        {
            var userid = await _userManager.FindByIdAsync(id);
            var user = await _userManager.FindByNameAsync(userid.UserName);

            user.UserName = username;
            user.Email = email;
            user.PhoneNumber = phonenumber;

            await _userManager.UpdateAsync(user);

        }

        public async Task DeleteUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userManager.GetUsersInRoleAsync("user");
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<int> GetTotalUserCount()
        {
            return await _userRepository.GetExistingUserQuantity();
        }

        public async Task<User> CreateUser(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                User createdUser = await _userManager.FindByEmailAsync(user.Email);
                _logger.Info($"Created user with email {user.Email}");

                return createdUser;
            }

            return null;
        }

        public async Task AddRole(User user, string role)
        {
            _logger.Info($"Added {role} role to user {user.Email}");
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task AddClaim(User createdUser, Claim claim)
        {
            _logger.Info($"Added [{claim.Type}:{claim.Value}] claim to user {createdUser.Email}");
            await _userManager.AddClaimAsync(createdUser, claim);
        }

        public async Task GenerateCard(User createdUser)
        {
            await _bankAccountService.GenerateBankAccount(createdUser, _currencyService.GetDefaultCurrency());

        }

        public async Task<IEnumerable<City>> GetUserCities()
        {
            IEnumerable<City> cities = await _cityRepository.GetAllAsync();
            return cities;
        }

        public async Task<User> GetUser(ClaimsPrincipal User)
        {
            return await _userManager.GetUserAsync(User);
        }

        public async Task<IdentityResult> ChangePassword(User user, string currentPassword, string newPassword)
        {
            return await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        }
        public async Task<User> GetUserByPhone(string phoneOrEmail)
          => await _userManager.Users.FirstOrDefaultAsync(x => x.Phone.Contains(phoneOrEmail));

        public async Task<User> GetById(int id)
        {
            User user = await _userRepository.GetUserById(id);
            return user;
        }

        public async Task<bool> UserExists(int id)
        {
            User user = await _userRepository.GetUserById(id);
            return user != null;
        }

        public async Task Update(User user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task<IEnumerable<Account>> GetUserAccounts(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            var accounts = await _accountRepository.GetAllAsync();

            return accounts.Where(a => a.UserId == id);
        }

        public async Task<Question> GetUserQuestion(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            var questions = await _questionRepository.GetAllAsync();
            return questions.FirstOrDefault(q => q.Id == user.QuestionId);
        }

        public async Task<User> GetCurrentUser(ClaimsPrincipal cp)
        {
            return await _userManager.GetUserAsync(cp);
        }

        public async Task<User> GetUserByPassportId(string passportId)
        {
            return await _userRepository.GetUserByPassportId(passportId);
        }

        public async Task<List<User>> GetUserListWhoMadeTransaction()
        {
            return await _userRepository.GetUserListWithTransactions();
        }
        public async Task ResetPassword(User user, string password)
        {
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            await _userManager.ResetPasswordAsync(user, token, password);
        }
    }
}
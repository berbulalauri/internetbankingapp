using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<IdentityRole<int>> _rolesManager;
        private readonly ILogger _logger;

        public SecurityService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole<int>> rolesManager, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesManager = rolesManager;
            _logger = logger;
        }

        public async Task<bool> Login(string email, string password)
        {
            SignInResult result = new SignInResult();
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                user.SecurityStamp = Guid.NewGuid().ToString();// proceed
                result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            }

            _logger.Info($"User with email {email} {(result.Succeeded ? "successfuly signed in" : "failed sign-in")}");

            return result.Succeeded;
        }

        public async Task Logout()
        {
            _logger.Info($"User {_signInManager.Context.User.Identity.Name} logged out");
            await _signInManager.SignOutAsync();
        }
        public async Task<bool> LoginWithPhone(string PhoneNumber, string password)
        {
            SignInResult result = new SignInResult();
            var user = _userManager.Users.FirstOrDefault(x => x.Phone.Contains(PhoneNumber));

            if (user != null && user.AllowPhoneForLogin==true)
            {
                user.SecurityStamp = Guid.NewGuid().ToString();
                result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            }

            _logger.Info($"User with PhoneNumber {PhoneNumber} {(result.Succeeded ? "successfuly signed in" : "failed sign-in")}");

            return result.Succeeded;
        }
    }
}

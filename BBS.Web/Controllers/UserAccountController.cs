using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BBS.DAL.Constants;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Constants;
using BBS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LoginViewModel = BBS.Web.Models.LoginViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using BBS.Models.Constants;

namespace BBS.Web.Controllers
{	
    [AllowAnonymous]
    public class UserAccountController : Controller
    {
    	private readonly IMailService _mailService;
        private IUserService _userService;
        private ISecurityService _securityService;
        private IQuestionService _questionService;
        private ILogger _logger;
        private readonly SignInManager<User> _signInManager;

		public UserAccountController(IMailService mail, IUserService userService,
            ISecurityService securityService, IQuestionService questionService,
            SignInManager<User> signInManager, ILogger logger)
        {
			_mailService = mail;
            _userService = userService;
            _securityService = securityService;
            _questionService = questionService;
            _logger = logger;
            _signInManager = signInManager;

        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                bool result;
                result = model.Identifier == BankStringConstants.PhoneNumberLogin
                        ? await _securityService.LoginWithPhone(model.PhoneOrEmail, model.Password)
                        : await _securityService.Login(model.PhoneOrEmail, model.Password);

                if (result)
                {
                    User user = model.Identifier == BankStringConstants.PhoneNumberLogin
                        ? await _userService.GetUserByPhone(model.PhoneOrEmail)
                        : await _userService.GetUserByEmail(model.PhoneOrEmail);

                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    
                    return Redirect(nameof(RedirectAfterLogin));
                }
                ModelState.AddModelError("", "invalid login attempt");
            }
            return View(model);
        }

        public IActionResult RedirectAfterLogin() 
        {
            return User.IsInRole(Roles.Admin)
                ? RedirectToAction("Index", "CustomLoan", new { area = "Administration" })
                : RedirectToAction("Index", "Cards");
        }

        public IActionResult AddQuestion()
        {
            var questions = _questionService.GetList().GetAwaiter().GetResult().Select(x => x.Content).ToList();

            ViewData["Questions"] = new SelectList(questions);
            return View();
        }

        public IActionResult FirstStepSignUp()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FirstStepSignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {
                if (await _userService.GetUserByEmail(signUpViewModel.Email) != null)
                {
                    ModelState.AddModelError("Email", ModelErrors.EmailExists);
                }
                else if (await _userService.GetUserByPhone(signUpViewModel.Phone) != null)
                {
                    ModelState.AddModelError("Phone", ModelErrors.PhoneExists);
                }
                else
                {
                    return RedirectToAction(nameof(SecondStepSignUp), signUpViewModel);
                }
            }
            return View();
        }


        
        public async Task<IActionResult> SecondStepSignUp(SignUpViewModel signUpViewModel)
        {
            SignUpContinuedViewModel signUpContinuedViewModel = new SignUpContinuedViewModel
            {
                FirstName = signUpViewModel.FirstName,
                LatsName = signUpViewModel.LatsName,
                Phone = signUpViewModel.Phone,
                Email = signUpViewModel.Email
            };

            var cities = await _userService.GetUserCities();
            ViewData["Cities"] = cities;
            var questions = await _questionService.GetList();
            ViewData["Questions"] = questions;
            return View(signUpContinuedViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SecondStepSignUp(SignUpContinuedViewModel signUpContinuedViewModel)
        {
            if (ModelState.IsValid)
            {
                

                User tempUser = await _userService.GetUserByPassportId(signUpContinuedViewModel.PassportId);
                if (tempUser != null)
                {
                    ModelState.AddModelError("PassportId", ModelErrors.PassportIdExists);
                }
                else
                {
                    User user = signUpContinuedViewModel;
                    User createdUser = await _userService.CreateUser(user, signUpContinuedViewModel.Password);

                    if (createdUser != null)
                    {
                        var role = createdUser.FirstName.Contains(Roles.Admin) ? Roles.Admin : Roles.User;
                        await _userService.AddRole(createdUser, role);
                        await _userService.AddClaim(createdUser, new Claim(ClaimTypes.Name, createdUser.FirstName));
                        await _userService.GenerateCard(createdUser);

	        			await _mailService.SignupWelcome(user.Email, user.FirstName+" "+user.LatsName);
                        return RedirectToAction(nameof(Login));
                    }
                }
            }

            var cities = await _userService.GetUserCities();
            ViewData["Cities"] = cities;
            var questions = await _questionService.GetList();
            ViewData["Questions"] = questions;
            return View();
        }
        
          

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _securityService.Logout();

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Update()
        {
            User currentUser = await _userService.GetCurrentUser(User);
            if (currentUser.FirstName.Contains(Roles.Admin))
            {
                return NotFound();
            }
            User userIncludingAccounts = await _userService.GetById(currentUser.Id);

            if (userIncludingAccounts == null)
            {
                return NotFound();
            }

            ViewData["Questions"] = await _questionService.GetList();


            return View(userIncludingAccounts);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(User user)
        {
            User currentUser = await _userService.GetCurrentUser(User);
            user.Id = currentUser.Id;

            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Update(user);
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exists = await _userService.UserExists(user.Id);
                    if (!exists)
                    {
                        return NotFound();
                    }
                }
                TempData["SuccessMessage"] = Messages.SuccessMessage;
                return RedirectToAction(nameof(Update));
            }
            return View(user);
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            User user = await _userService.GetByEmail(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "User with this email is not registered!");
                return View();
            }
            else if (model.Phone != user.Phone)
            {
                ModelState.AddModelError("Phone", "Incorrect Phone number!");
                return View();
            }
            ForgotPasswordSecondStepViewModel forgotPasswordSecondStepViewModel = new ForgotPasswordSecondStepViewModel() { Email = user.Email };
            return RedirectToAction(nameof(SecretQuestionConfirmation), forgotPasswordSecondStepViewModel);
        }

        public async Task<IActionResult> SecretQuestionConfirmation(ForgotPasswordSecondStepViewModel model)
        {
            User user = await _userService.GetByEmail(model.Email);
            ViewData["Question"] = user.Question;
            SecretQuestionConfirmationViewModel secretQuestionConfirmationViewModel = new SecretQuestionConfirmationViewModel() { UserId = user.Id };
            return View(secretQuestionConfirmationViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> SecretQuestionConfirmation(SecretQuestionConfirmationViewModel model)
        {
            User user = await _userService.GetById(model.UserId);
            if (user.Answer != model.UserAnswer)
            {
                ModelState.AddModelError("UserAnswer", "Incorrect answer!");
                ViewData["Question"] = user.Question;
                return View();
            }

            ForgotPasswordThirdStepViewModel forgotPasswordThirdStepViewModel = new ForgotPasswordThirdStepViewModel() { UserId = user.Id };

            return RedirectToAction(nameof(ForgotPasswordConfirmation), forgotPasswordThirdStepViewModel);
        }
        public IActionResult ForgotPasswordConfirmation(ForgotPasswordThirdStepViewModel model)
        {
           
            ForgotPasswordConfirmationViewModel forgotPasswordConfirmationViewModel = new ForgotPasswordConfirmationViewModel() { UserId = model.UserId };
            return View(forgotPasswordConfirmationViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPasswordConfirmation(ForgotPasswordConfirmationViewModel model)
        {
            User user = await _userService.GetById(model.UserId);
            await _userService.ResetPassword(user, model.NewPassword);
            await _mailService.PasswordChange(user.Email, "Your password has been updated!");

            ViewData["PasswordResetedSuccessfully"] = "Password has been reseted successfully";
            return View();
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetCurrentUser(User);
                if (user == null)
                {
                    return View();
                }

                var result = await _userService.ChangePassword(user,
                    model.CurrentPassword, model.NewPassword);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _signInManager.RefreshSignInAsync(user);
                await _mailService.PasswordChange(user.Email, user.FirstName);
                
                TempData["SuccessMessage"] = Messages.SuccessMessage;
                return RedirectToAction("Update");
            }

            return View(model);
        }
    }
}
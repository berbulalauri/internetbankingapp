using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Constants;
using BBS.Models.Models;
using BBS.Web.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{
	public class InitTestDataController : Controller
	{
		private readonly ITestDataInitializerService _testDataInitializer;
		private readonly UserManager<User> _userManager;
		private readonly ILogger _logger;
		public InitTestDataController(ITestDataInitializerService testDataInitializer,
			UserManager<User> userManager,ILogger logger)
		{
			_testDataInitializer = testDataInitializer;
			_userManager = userManager;
			_logger = logger;
		}
		public async Task<IActionResult> Index()
		{
			await CreateUserWithRole();
			//await CreateDepositTypes();
			await CreateLoanTypes();
			await CreateAccount();
			await CreateCard();
			await CreateServices();

			return RedirectToAction("Login", "UserAccount");
		}

		public async Task CreateUserWithRole()
		{
			User testUser = new User()
			{
				UserName = "TestingTest",
				NormalizedUserName = "TESTING TEST",
				Email = "test@gmail.com",
				NormalizedEmail = "TEST@GMAIL.COM",
				FirstName = "Testing",
				LatsName = "Test",
				PhoneNumber = "591 97 12 02",
				PassportId = "01020325658",
				Address = "Politkovskaia street", // Add all data to avoid NullReferenceExceptions
				Status = "Active",
				QuestionId = 1,
				Answer = "13",
				CityId = 1,
				RegDate = new DateTime(2020, 1, 27),
				PasswordHash = "1t1sMyUs&r"
			};

			testUser.PasswordHash = new PasswordHasher<IdentityUser<int>>().HashPassword(testUser, testUser.PasswordHash);

			var result = await _userManager.CreateAsync(testUser);
			if (result.Succeeded)
			{
				var createdUser = await _userManager.FindByNameAsync(testUser.UserName);
				var role = testUser.UserName.Contains(Roles.Admin) ? Roles.Admin : Roles.User;
				await _userManager.AddToRoleAsync(createdUser, role);
				_logger.Info($"Create Account  {createdUser} from {role} ");
			}
		}

		public async Task CreateDepositTypes()
		{
			await _testDataInitializer.CreateDepositType(
					new DepositType
					{
						AnnualRate = 5.7M,
						BenefitsDescription = "It is cool",
						CurrencyId = 1,
						BonusRate = 3,
						Description = "It is TBCgti's best deposit",
						InterestPaymentId = 1,
						MaximumDepositAmount = 60000,
						MinimumDepositAmount = 1500,
						MinimumReplenishmentAmount = 1800,
						Name = "Guaranted Capital"
					}
				);
			await _testDataInitializer.CreateDepositType(
					new DepositType
					{
						AnnualRate = 5.2M,
						BenefitsDescription = "It is cool also",
						CurrencyId = 1,
						BonusRate = 3,
						Description = "It is TBCgti's deposit",
						InterestPaymentId = 2,
						MaximumDepositAmount = 30000,
						MinimumDepositAmount = 1000,
						MinimumReplenishmentAmount = 1200,
						Name = "Reliable Income"
					});
			_logger.Info($"Create Deposit Type   ");
		}

		public async Task CreateLoanTypes()
		{
			await _testDataInitializer.CreateLoanType(
					new LoanType
					{
						Name = "Cash Loan Up to 20 000 GEL",
						CurrencyId = 1,
						MaxLoanSum = 20000,
						MinLoanSum = 1000,
						InterestRate = 67,
						FeeForInsuranceLoan = 0,
						MinTerm = 3,
						MaxTerm = 60,
						PaymentForAccidentInsurance = 80,
						MonthlyFee = 600,
						FreeForServicesUponReciptCash = 3,
						//DebtsRepaymentSchedule = new DateTime(2020, 2, 28), // CHANGE AFTER CREATING CORRECT FIELD-TYPE
						//AdvancedRepayment = 0,
						GeneralInformation = "Simple and transparent conditions and terms in the popular stable bank",
						LoanSummary = "Get up to 20 000 GEL in a very short time"
					});
			await _testDataInitializer.CreateLoanType(
				new LoanType
				{
					Name = "Card With Limit",
					CurrencyId = 1,
					MaxLoanSum = 5000,
					MinLoanSum = 1000,
					InterestRate = 55,
					FeeForInsuranceLoan = 0,
					MinTerm = 3,
					MaxTerm = 48,
					PaymentForAccidentInsurance = 70,
					MonthlyFee = 500,
					FreeForServicesUponReciptCash = 3,
					//DebtsRepaymentSchedule = new DateTime(2020, 2, 28), // CHANGE AFTER CREATING CORRECT FIELD-TYPE
					//AdvancedRepayment = 0,
					GeneralInformation = "Simple and transparent conditions and terms in the popular stable bank",
					LoanSummary = "Get up to 5 000 GEL in a very short time"
				});
			_logger.Info($"Create Loan Type   ");
		}

		public async Task CreateAccount()
		{
			await _testDataInitializer.CreateAccount(
					new Account
					{
						Balance = 3000,
						CurrencyId = 1,
						Name = "Test Account",
						Number = "GE001TBCGTI000001425566",
						RegDate = new DateTime(2020, 1, 26),
						Status = "Active",
						UserId = 2
					}
				);
			await _testDataInitializer.CreateAccount(
					new Account
					{
						Balance = 200,
						CurrencyId = 1,
						Name = BankStringConstants.PhoneRefillAcc,
						Number = "GE001TBCGTI00005342675",
						RegDate = new DateTime(2009, 1, 9),
						Status = "Active",
						UserId = 2
					}
				);
		}

		public async Task CreateCard()
		{
			await _testDataInitializer.CreateCard(
					new Card
					{
						UserId=2,
						AccountId = 13,
						Cvc2 = 123,
						ExpDate = new DateTime(2022, 2, 22),
						IsDefault = true,
						Name = "Test Card",
						Number = "1234567891234567",
						RegDate = new DateTime(2020, 1, 26),
						Satus = true,
						Status = "Active"
					}
				);
			_logger.Info($"Create Card   ");
		}

		public async Task CreateServices()
		{
			await _testDataInitializer.CreateServiceCategory(new ServiceCategory {
				Name = "Mobile and Internet",
			});

			await _testDataInitializer.CreateServiceCategory(new ServiceCategory
			{
				Name = "General Banking",
			});

			await _testDataInitializer.CreateService(new Service {
				CategoryId = 1,
				FixedFee = 0.50M,
				AccountId = 14,
				Name = BankStringConstants.PhoneRefilServiceName,
				PercentFee = 0M,
			});


			await _testDataInitializer.CreateService(new Service
			{
				CategoryId = 2,
				FixedFee = 0,
				Name = BankStringConstants.GeneralTransferServiceDescription,
				PercentFee = 0.05M,
			});
		}
	}
}
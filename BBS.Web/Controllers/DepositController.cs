using BBS.Core.Services;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BBS.Web.Controllers
{
    [Authorize]
    public class DepositController : Controller
    {
        private readonly IDepositService _depositService;
        private readonly IBankAccountService _bankAccountService;
        private readonly ICurrencyService _currencyService;
        private readonly IDepositTermService _depositTermService;
        private readonly UserManager<User> _userManager;
        private readonly ITestDataInitializerService _testDataInitializerService;

        public DepositController(IDepositService depositService,
            ICurrencyService currencyService,
            IDepositTermService depositTermService,
            IBankAccountService bankAccountService,
            UserManager<User> userManager,
           ITestDataInitializerService iTestDataInitializerService)
        {
            _depositService = depositService;
            _currencyService = currencyService;
            _depositTermService = depositTermService;
            _bankAccountService = bankAccountService;
            _userManager = userManager;
            _testDataInitializerService = iTestDataInitializerService;
        }


        public async Task<IActionResult> OpenAsync(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var depositTypes = await _depositService.GetAllTypes();
            var depositType = depositTypes.FirstOrDefault(x => x.Id == id);
            ViewData["DepositTypeId"] = depositType;
            ViewData["DepositTermId"] = new SelectList(await _depositTermService.GetAllWithTerm(), "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["AccountToTransferId"] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData["AccountForInterestId"] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData["InterestRate"] = new Dictionary<int, string>(depositTypes.Select(x => new KeyValuePair<int, string>(x.Id, $"{x.AnnualRate}%")));

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OpenAsync(Deposit deposit)
        {
            var user = await _userManager.GetUserAsync(User);
            deposit.Id = 0;
            var account = _bankAccountService.GetUserAccounts(user.Id).GetAwaiter().GetResult().FirstOrDefault(x => x.Id == deposit.AccountToTransferId);
            var depositTypes = await _depositService.GetAllTypes();
            try
            {
                if (ModelState.IsValid)
                {
                        await _depositService.OpenDepositAsync(deposit, user.Id, account);
                    ViewData["SuccessMessage"] = "Updated successfully";
                    return RedirectToAction("Index", "Cards");
                }
            }
            catch (NotEnoughMoneyException)
            {
                ViewData["AboutDeposit"] = "You have not enough money in deposit";
            }

            var depositsTypes = await _depositService.GetAllTypes();
            var depositType = depositsTypes.FirstOrDefault(x => x.Id == deposit.DepositTypeId);
            ViewData["DepositTypeId"] = depositType;
            ViewData["DepositTermId"] = new SelectList(await _depositTermService.GetAllWithTerm(), "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["AccountToTransferId"] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData["AccountForInterestId"] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData["InterestRate"] = new Dictionary<int, string>(depositTypes.Select(x => new KeyValuePair<int, string>(x.Id, $"{x.AnnualRate}%")));
            return View("Open");
        }



        [HttpGet]
        public async Task<IActionResult> Index(string amountString, string currencyString, string termString)
        {
            var depositTypes = await _depositService.GetAllTypes();
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["TermId"] = new SelectList(await _depositTermService.GetAllWithTerm(), "Id", "Name");
            ViewData["AmountFilter"] = amountString;
            ViewData["CurrencyFilter"] = currencyString;
            ViewData["TermFilter"] = termString;

            bool isCurrencyInt = int.TryParse(currencyString, out int searchCurrency);
            bool isTermInt = int.TryParse(termString, out int searchTerm);

            if (decimal.TryParse(amountString, out decimal searchAmount) || isCurrencyInt || isTermInt)
            {
                var depositTypes2 = await _depositService.GetWithFilter(searchAmount, searchCurrency, searchTerm);

                return View(depositTypes2);
            }

            return View(depositTypes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var depoType = await _depositService.GetType(id);

            if (depoType == null)
            {
                return NotFound();
            }

            return View(depoType);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult DepositeManagment()
        {
            return View();
        }
    }
}
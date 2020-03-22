using BBS.Core.CustomExceptions;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Constants;
using BBS.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{
    [Authorize]
    public class LoanRequestController : Controller
    {
        private readonly IEmploymentTypeService _employmentTypeService;
        private readonly ILoanTypeService _loanTypeService;
        private readonly UserManager<User> _userManager;
        private readonly ILoanRequestService _loanRequestService;
        private readonly IBankAccountService _bankAccountService;
        private readonly IEmploymentService _employmentService;

        public LoanRequestController(IEmploymentTypeService employmentTypeService,
            ILoanTypeService loanTypeService, ICurrencyService currencyService,
            UserManager<User> userManager, ILoanRequestService loanRequestService,
            IBankAccountService bankAccountService, IEmploymentService employmentService)
        {
            _employmentTypeService = employmentTypeService;
            _loanTypeService = loanTypeService;
            _userManager = userManager;
            _loanRequestService = loanRequestService;
            _bankAccountService = bankAccountService;
            _employmentService = employmentService;
        }

        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            ViewData[ViewDataKeys.UserAccountId] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData[ViewDataKeys.LoanType] = await _loanTypeService.GetLoanType((int)id);
            ViewData[ViewDataKeys.EmploymentTypeId] = new SelectList(_employmentTypeService.GetAllTypes(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, EmploymentLoanRequestViewModel employmentLoanRequest)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var loanType = await _loanTypeService.GetLoanType(id);

            try
            {
                if (ModelState.IsValid)
                {
                    await _employmentService.CreateEmployment(employmentLoanRequest.Employment);

                    var employments = await _employmentService.GetEmployments();
                    var employmentId = employments.OrderByDescending(x => x.Id).FirstOrDefault().Id;
                    await _loanRequestService.CreateLoanRequest(employmentLoanRequest.LoanRequest, loanType, user.Id, employmentId);

                    ViewData[ViewDataKeys.LoanRequestSuccess] = "Loan Request Sent Successfully!";
                }
            }
            catch (LoanTermOutOfBoundException)
            {
                ViewData[ViewDataKeys.LoanTermException] = $"Loan Term must be between {loanType.MinTerm}-{loanType.MaxTerm} months";
            }
            catch (LoanSumOutOfBoundException)
            {
                ViewData[ViewDataKeys.LoanSumException] = $"Loan Sum must be between {loanType.MinLoanSum}-{loanType.MaxLoanSum} {loanType.Currency.Name}";
            }

            ViewData[ViewDataKeys.UserAccountId] = new SelectList(await _bankAccountService.GetUserAccounts(user.Id), "Id", "Name");
            ViewData[ViewDataKeys.LoanType] = await _loanTypeService.GetLoanType(1);
            ViewData[ViewDataKeys.EmploymentTypeId] = new SelectList(_employmentTypeService.GetAllTypes(), "Id", "Name");

            return View();
        }

        public async Task<IActionResult> GetAllUsersLoan()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var loanRequests = await _loanRequestService.GetAllUsersLoan(user.Id);

            return View(loanRequests);
        }
    }
}
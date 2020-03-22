using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Areas.Administration.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{
    [Area(AdministratorArea.Name)]
    [Authorize(Policy = AdministratorArea.Policy)]
    public class LoanTypeManagmentController : Controller
    {
        private readonly ILoanTypeService _loanTypeService;
        private readonly ICurrencyService _currencyService;

        public LoanTypeManagmentController(ILoanTypeService loanTypeService, ICurrencyService currencyService)
        {
            _loanTypeService = loanTypeService;
            _currencyService = currencyService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _loanTypeService.GetLoanTypes());
        }


        public async Task<IActionResult> Details(int id)
        {
            var loanType = await _loanTypeService.GetLoanType(id);

            if (loanType == null)
            {
                return NotFound();
            }

            return View(loanType);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,GeneralInformation,MinLoanSum,MaxLoanSum,InterestRate,LoanSummary,MonthlyFee,MinTerm,MaxTerm,PaymentForAccidentInsurance,FeeForInsuranceLoan,FreeForServicesUponReciptCash,CurrencyId")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                await _loanTypeService.AddLoan(loanType);
                return RedirectToAction(nameof(Index));
            }


            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            return View(loanType);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var loanType = await _loanTypeService.GetLoanType(id);
            if (loanType == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            return View(loanType);
        }

        [HttpPost]
        public async Task<IActionResult> Update([Bind("Id,Name,Description,GeneralInformation,MinLoanSum,MaxLoanSum,InterestRate,LoanSummary,MonthlyFee,MinTerm,MaxTerm,PaymentForAccidentInsurance,FeeForInsuranceLoan,FreeForServicesUponReciptCash,CurrencyId")] LoanType loanType)
        {
            if (ModelState.IsValid)
            {
                await _loanTypeService.UpdateLoanType(loanType);

                return RedirectToAction(nameof(Index));
            }

            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            return View(loanType);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var loanType = await _loanTypeService.GetLoanType(id);

            if (loanType == null)
            {
                return NotFound();
            }

            return View(loanType);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _loanTypeService.DeleteLoanType(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
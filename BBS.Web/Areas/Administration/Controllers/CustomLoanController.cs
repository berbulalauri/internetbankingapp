using BBS.Interfaces.Services;
using BBS.Web.Areas.Administration.Constants;
using BBS.Web.Areas.Administration.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BBS.Web.Areas.Administration.Controllers
{
    [Area(AdministratorArea.Name)]
    [Authorize(Policy = AdministratorArea.Policy)]
    public class CustomLoanController : Controller
    {
        private readonly ILoanRequestService _loanRequestService;
        private readonly ILoanService _loanService;

        public CustomLoanController(ILoanRequestService loanRequestService, ILoanService loanService)
        {
            _loanRequestService = loanRequestService;
            _loanService = loanService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CustomLoansOnReview"] = await _loanRequestService.GetAllOnReviewByAdmin();
            ViewData["AcceptedCustomLoans"] = await _loanRequestService.GetAllAcceptedByAdmin();
            ViewData["RejectedCustomLoans"] = await _loanRequestService.GetAllRejectedByAdmin();

            return View();
        }

        public async Task<IActionResult> AcceptRequest(int? id)
        {
            if (id != null)
            {
                var loanRequest = await _loanRequestService.GetById((int)id);

                if (loanRequest != null)
                {
                    await _loanService.CreateLoan(loanRequest.Id);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeclineRequest([Bind("Comment")] CustomLoanViewModel customLoanViewModel, int? id)
        {
            if (id != null)
            {
                var loanRequest = await _loanRequestService.GetById((int)id);

                if (ModelState.IsValid)
                {
                    if (loanRequest != null)
                    {
                        await _loanRequestService.DeclineLoanRequest(loanRequest, customLoanViewModel.Comment);
                    }
                }
                else
                {
                    TempData["Comment"] = "Comment is REQUIRED while rejecting loan request";
                }
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces.Services;
using BBS.Web.Areas.Administration.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using BBS.Models.Models;

namespace BBS.Web.Areas.Administration.Controllers
{
    [Area(AdministratorArea.Name)]
    [Authorize(Policy = AdministratorArea.Policy)]
    public class DashboardController : Controller
    {
        private const int bankAccountId = 1;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(IUnitOfWork unitOfWork, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string questionText1, string questionText2, string questionText3,
                                               string questionText4, string order = "Id", bool ascend = true)
        {
            var bankAccountInfo = await _unitOfWork.AccountRepository.GetByIdAsync(bankAccountId);
            ViewData["bankCurrentBalance"] = bankAccountInfo.Balance;
            ViewData["userList"] = await _userService.GetUserListWhoMadeTransaction();
            ViewData["depositList"] = await _unitOfWork.DepositRepository.GetAllWithTrackingAsync();
            ViewData["LoanList"] = await _unitOfWork.LoanRepository.GetAllLoans();
            ViewData["transactList"] = await _unitOfWork.TransactionsRepository.TransactionsWithInclides();
            ViewData["getDepoCount"] = await _unitOfWork.DepositRepository.GetTotalDepositCount();
            ViewData["getLoanCount"] = await _unitOfWork.LoanRepository.GetTotalLoanCount();
            ViewData["getUserCount"] = await _userService.GetTotalUserCount();
            ViewData["getDepoAmount"] = await _unitOfWork.DepositRepository.GetTotalDepositAmount();
            ViewData["getLoanAmount"] = await _unitOfWork.LoanRepository.GetTotalLoanAmount();

            List<User> usersWithCompletedTransactions = await _userService.GetUserListWhoMadeTransaction();
            List<Deposit> getAllExistingDeposits = await _unitOfWork.DepositRepository.GetAllWithTrackingAsync();
            List<Loan> getAllExistingLoans = await _unitOfWork.LoanRepository.GetAllLoans();
            List<Transactions> getAllExistingTransactions = await _unitOfWork.TransactionsRepository.TransactionsWithInclides();

            var resultOfusersWithCompletedTransactions = !string.IsNullOrWhiteSpace(questionText1) ?
                usersWithCompletedTransactions.Where(x =>
                x.UserName.Contains(questionText1) ||
                x.FirstName.Contains(questionText1) ||
                x.LatsName.Contains(questionText1) ||
                x.Email.Contains(questionText1) ||
                x.Address.Contains(questionText1)) : usersWithCompletedTransactions;

            var resultOfgetAllExistingDeposits = !string.IsNullOrWhiteSpace(questionText2) ?
                getAllExistingDeposits.Where(x =>
                x.DepositType.Name.Contains(questionText2) ||
                x.DepositAmount.Equals(questionText2) ||
                x.Currency.Name.Contains(questionText2) ||
                x.User.FirstName.Contains(questionText2) ||
                x.AccountForInterest.Name.Contains(questionText2) ||
                x.DepositTerm.Name.Contains(questionText2)) : getAllExistingDeposits;

            var resultOfgetAllExistingLoans = !string.IsNullOrWhiteSpace(questionText3) ?
                getAllExistingLoans.Where(x =>
                x.LoanRequest.LoanSum.Equals(questionText3) ||
                x.Status.Contains(questionText3) ||
                x.LoanRequest.LoanType.Name.Contains(questionText3) ||
                x.Date.Equals(questionText3)) : getAllExistingLoans;

            var resultOfgetAllExistingTransactions = !string.IsNullOrWhiteSpace(questionText4) ?
                getAllExistingTransactions.Where(x =>
                x.SenderCard.Name.Contains(questionText4) ||
                x.ReceiverAccount.Name.Contains(questionText4) ||
                x.Amount.Equals(questionText4) ||
                x.Service.Name.Contains(questionText4) ||
                x.PersonalAccountId.Contains(questionText4)) : getAllExistingTransactions;

            PropertyInfo propertyOfUser = typeof(User).GetProperty(order);
            PropertyInfo propertyOfDeposit = typeof(Deposit).GetProperty(order);
            PropertyInfo propertyOfLoan = typeof(Loan).GetProperty(order);
            PropertyInfo propertyOfTransactions = typeof(Transactions).GetProperty(order);

            if (propertyOfUser != null)
            {
                ViewData["transactOfUsers"] = (ascend) ? resultOfusersWithCompletedTransactions.OrderBy(x => propertyOfUser.GetValue(x)).ToList()
                                                : resultOfusersWithCompletedTransactions.OrderByDescending(x => propertyOfUser.GetValue(x)).ToList();
            }
            else
            {
                ViewData["transactOfUsers"] = usersWithCompletedTransactions.OrderBy(x => x.Id).ToList();
            }

            if (propertyOfDeposit != null)
            {
                ViewData["transactOfDeposit"] = (ascend) ? resultOfgetAllExistingDeposits.OrderBy(x => propertyOfDeposit.GetValue(x)).ToList()
                                                : getAllExistingDeposits.OrderByDescending(x => propertyOfDeposit.GetValue(x)).ToList();
            }
            else
            {
                ViewData["transactOfDeposit"] = getAllExistingDeposits.OrderBy(x => x.Id).ToList();
            }

            if (propertyOfLoan != null)
            {
                ViewData["transactOfLoan"] = (ascend) ? resultOfgetAllExistingLoans.OrderBy(x => propertyOfLoan.GetValue(x)).ToList()
                                                : resultOfgetAllExistingLoans.OrderByDescending(x => propertyOfLoan.GetValue(x)).ToList();
            }
            else
            {
                ViewData["transactOfLoan"] = getAllExistingLoans.OrderBy(x => x.Id).ToList();
            }

            if (propertyOfTransactions != null)
            {
                ViewData["transactOfTransactions"] = (ascend) ? resultOfgetAllExistingTransactions.OrderBy(x => propertyOfTransactions.GetValue(x)).ToList()
                                                : resultOfgetAllExistingTransactions.OrderByDescending(x => propertyOfTransactions.GetValue(x)).ToList();
            }
            else
            {
                ViewData["transactOfTransactions"] = getAllExistingTransactions.OrderBy(x => x.Id).ToList();
            }

            ViewData["orderInfo"] = new Tuple<string, bool>(order, ascend);
            return View();
        }
    }
}
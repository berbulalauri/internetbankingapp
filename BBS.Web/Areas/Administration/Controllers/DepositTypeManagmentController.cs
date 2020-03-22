using BBS.Interfaces.Services;
using BBS.Models.Models;
using BBS.Web.Areas.Administration.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using BBS.Models.Helpers;
using System;
using BBS.Models.Constants;

namespace BBS.Web.Areas.Administration.Controllers
{
    [Area(AdministratorArea.Name)]
    [Authorize(Policy = AdministratorArea.Policy)]
    public class DepositTypeManagmentController : Controller
    {
        private readonly IDepositService _depoService;
        private readonly ICurrencyService _currencyService;
        private readonly IInterestPaymentTypeService _interestPaymentService;
        private readonly IDepositTermService _depositTerm;

        public DepositTypeManagmentController(IDepositService deposit,
            ICurrencyService currencyService, IInterestPaymentTypeService interestPaymentType, IDepositTermService depositTerm)
        {
            _depoService = deposit;
            _currencyService = currencyService;
            _interestPaymentService = interestPaymentType;
            _depositTerm = depositTerm;
        }

        public async Task<IActionResult> Index(string sortOrder, string columnName = "Name", string searchString = "")
        {
            var columnNames = DepositTypeColumnNames.columnNames.ToList();

            ViewData["ColumnNames"] = new SelectList(columnNames, columnNames.FirstOrDefault(x => x == columnName));
            ViewData["SelectedColumn"] = columnName;
            ViewData["SelectedValue"] = searchString;

            ViewData["Name"] = sortOrder == Order.Name ? OrderDesc.Name : Order.Name;
            ViewData["Description"] = sortOrder == Order.Description ? OrderDesc.Description : Order.Description;
            ViewData["BenefitsDescription"] = sortOrder == Order.BenefitsDescription ? OrderDesc.BenefitsDescription : Order.BenefitsDescription;
            ViewData["AnnualRate"] = sortOrder == Order.AnnualRate ? OrderDesc.AnnualRate : Order.AnnualRate;
            ViewData["BonusRate"] = sortOrder == Order.BonusRate ? OrderDesc.BonusRate : Order.BonusRate;
            ViewData["MinimumDepositAmount"] = sortOrder == Order.MinimumDepositAmount ? OrderDesc.MinimumDepositAmount : Order.MinimumDepositAmount;
            ViewData["MinimumReplenishmentAmount"] = sortOrder == Order.MinimumReplenishmentAmount ? OrderDesc.MinimumReplenishmentAmount : Order.MinimumReplenishmentAmount;
            ViewData["MaximumDepositAmount"] = sortOrder == Order.MaximumDepositAmount ? OrderDesc.MinimumReplenishmentAmount : Order.MaximumDepositAmount;
            ViewData["Currency"] = sortOrder == Order.Currency ? OrderDesc.Currency : Order.Currency;
            ViewData["InterestPaymentType"] = sortOrder == Order.InterestPaymentType ? OrderDesc.InterestPaymentType : Order.InterestPaymentType;
            ViewData["DepositTerm"] = sortOrder == Order.DepositTerm ? OrderDesc.DepositTerm : Order.DepositTerm;

            return View(await _depoService.Filter(sortOrder, columnName, searchString));
        }

        public async Task<IActionResult> Details(int id)
        {
            var depoType = await _depoService.GetType(id);

            if (depoType == null)
            {
                return NotFound();
            }

            return View(depoType);
        }

        // GET: OwnershipLists/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["InterestPaymentId"] = new SelectList(await _interestPaymentService.GetAll(), "Id", "Name");
            ViewData["DepositTermId"] = new SelectList(await _depositTerm.GetAllWithTerm(), "Id", "Name");


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,BenefitsDescription," +
            "AnnualRate,BonusRate,MinimumDepositAmount,MinimumReplenishmentAmount," +
            "MaximumDepositAmount,InterestPaymentId,CurrencyId,DepositTermId")] DepositType depositType)
        {
            if (ModelState.IsValid)
            {
                await _depoService.CreateDepositType(depositType);
                return RedirectToAction(nameof(Index));
            }
            ViewData["InterestPaymentId"] = new SelectList(await _interestPaymentService.GetAll(), "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["DepositTermId"] = new SelectList(await _depositTerm.GetAllWithTerm(), "Id", "Name");
            return View(depositType);
        }

        public IActionResult DepositTypeManagment()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var depoType = await _depoService.GetType(id);

            if (depoType == null)
            {
                return NotFound();
            }

            return View(depoType);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _depoService.Delete(id);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int id)
        {
            var depositTypeInfo = await _depoService.GetDepositTypeService(id);
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["InterestPaymentId"] = new SelectList(await _interestPaymentService.GetAll(), "Id", "Name");
            ViewData["DepositTermId"] = new SelectList(await _depositTerm.GetAllWithTerm(), "Id", "Name");

            if (depositTypeInfo == null)
            {
                return NotFound();
            }
            return View(depositTypeInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, [Bind("Id,Name,Description,BenefitsDescription," +
            "AnnualRate,BonusRate,MinimumDepositAmount,MinimumReplenishmentAmount," +
            "MaximumDepositAmount,InterestPaymentId,CurrencyId,DepositTermId")] DepositType depositType)
        {
            if (ModelState.IsValid)
            {
                await _depoService.UpdateDepositTypeService(depositType);
                return RedirectToAction(nameof(Index));
            }
            ViewData["InterestPaymentId"] = new SelectList(await _interestPaymentService.GetAll(), "Id", "Name");
            ViewData["CurrencyId"] = new SelectList(await _currencyService.GetAll(), "Id", "Name");
            ViewData["DepositTermId"] = new SelectList(await _depositTerm.GetAllWithTerm(), "Id", "Name");

            return View(depositType);
        }
    }
}
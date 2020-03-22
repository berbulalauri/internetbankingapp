using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace BBS.Web.Controllers
{
    [Authorize]
    public class LoanController : Controller
    {
        private readonly IEmploymentTypeService _IEmploymentTypeService;
        private readonly ILoanTypeService _loanService;
        private readonly ILoanTypeService _ILoanTypeService;
        private readonly ICurrencyService _ICurrencyService;
        private readonly ITestDataInitializerService ITestDataInitializerService;
        public LoanController(IEmploymentTypeService IEmploymentTypeService,
                              ILoanTypeService ILoanTypeService,
                              ICurrencyService ICurrencyService,
                              ITestDataInitializerService _ITestDataInitializerService, IUnitOfWork unitOfWork,
                               ILoanTypeService loanService)
        {
            ITestDataInitializerService = _ITestDataInitializerService;
            _ICurrencyService = ICurrencyService;
            _ILoanTypeService = ILoanTypeService;
            _IEmploymentTypeService = IEmploymentTypeService;
            _loanService = loanService;
        }
        public async Task<IActionResult> ChooseLoan()
        {

            ViewData["CurrencyId"] = new SelectList(await _ICurrencyService.GetAll(), "Id", "Name");
            var model = await _ILoanTypeService.GetLoanTypes();
            return View(model);
        }
        public async Task<IActionResult> LoanDetail(int id)
        {
            var loanType = await _ILoanTypeService.LoanTypeDetailsService(id);

            if (loanType == null)
            {
                return NotFound();
            }

            return View(loanType);
        }
        public IActionResult Add()
        {
            return View();
        }


        public IActionResult Order()
        {
            ViewData["EmploymentList"] = _IEmploymentTypeService.GetAllTypes();
            return View();
        }
    }
}
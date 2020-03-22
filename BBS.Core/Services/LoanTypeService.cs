using BBS.DAL.Repositories;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class LoanTypeService : ILoanTypeService
    {
        private readonly ILoanTypeRepository _loanTypeRepository;
        private ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        public LoanTypeService(ILoanTypeRepository loanTypeRepository, ILogger logger, IUnitOfWork unitOfWork)
        {
            _loanTypeRepository = loanTypeRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;

        }


        public async Task<IEnumerable<LoanType>> GetLoanTypes()
        {
            return await _loanTypeRepository.GetAllLoanTypeWithCurrencyAsync();
        }

        public async Task<LoanType> GetLoanType(int id)
        {

            _logger.Info($"Get Loan Types: {id.ToString()}");
            return await _loanTypeRepository.GetLoanTypeWithCurrencyByIdAsync(id);

        }

        public async Task AddLoan(LoanType loanType)
        {
            await _loanTypeRepository.InsertAsync(loanType);
            await _loanTypeRepository.SaveAsync();
            _logger.Info($"Add Loan Types: {loanType.Name}");
        }

        public async Task UpdateLoanType(LoanType loanType)
        {
            await _loanTypeRepository.UpdateAsync(loanType);
            await _loanTypeRepository.SaveAsync();
            _logger.Info($"Update Loan Types: {loanType.Name}");
        }

        public async Task DeleteLoanType(int id)
        {
            await _loanTypeRepository.DeleteAsync(id);
            await _loanTypeRepository.SaveAsync();
            _logger.Info($"Delete Loan Types {id.ToString()}");
        }
        public async Task<LoanType> LoanTypeDetailsService(int id)
        {
           return  _unitOfWork.LoanTypeRepository.GetByIdAsync(id);
        }
    }
}

using BBS.Core.CustomExceptions;
using BBS.DAL.Repositories;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class LoanRequestService : ILoanRequestService
    {
        private readonly ILoanRequestRepository _loanRequestRepository;
        private readonly ILogger _logger;

        public LoanRequestService(ILoanRequestRepository loanRequestRepository, ILogger logger)
        {
            _loanRequestRepository = loanRequestRepository;
            _logger = logger;
        }

        public async Task CreateLoanRequest(LoanRequest loanRequest, LoanType loanType,
            int userId, int employmentId)
        {
            if (loanRequest.LoanSum < loanType.MinLoanSum || loanRequest.LoanSum > loanType.MaxLoanSum)
            {
                throw new LoanSumOutOfBoundException();
            }
            if (loanRequest.Term < loanType.MinTerm || loanRequest.Term > loanType.MaxTerm)
            {
                throw new LoanTermOutOfBoundException();
            }

            loanRequest.DateOfRequest = DateTime.Now;
            loanRequest.LoanTypeId = loanType.Id;
            loanRequest.UserId = userId;
            loanRequest.EmployementId = employmentId;
            await _loanRequestRepository.InsertAsync(loanRequest);
            await _loanRequestRepository.SaveAsync();
            _logger.Info($"Created new loan request type: {loanType.Name}");
        }

        public async Task<IEnumerable<LoanRequest>> GetAllAcceptedByAdmin()
        {
            var allLoanRequest = await _loanRequestRepository.GetAllWithFullInfo();
            return allLoanRequest.Where(lr => lr.IsAccepted && !lr.IsDenied);
        }

        public async Task<IEnumerable<LoanRequest>> GetAllOnReviewByAdmin()
        {
            var allLoanRequest = await _loanRequestRepository.GetAllWithFullInfo();
            return allLoanRequest.Where(lr => !lr.IsAccepted && !lr.IsDenied);
        }

        public async Task<IEnumerable<LoanRequest>> GetAllRejectedByAdmin()
        {
            var allLoanRequest = await _loanRequestRepository.GetAllWithFullInfo();
            return allLoanRequest.Where(lr => !lr.IsAccepted && lr.IsDenied);
        }

        public async Task<LoanRequest> GetById(int id)
        {
            return await _loanRequestRepository.GetByIdAsync(id);
        }

        public async Task DeclineLoanRequest(LoanRequest loanRequest, string comment)
        {
            loanRequest.IsDenied = true;
            loanRequest.Comments = comment;
            await _loanRequestRepository.SaveAsync();
        }

        public async Task<IEnumerable<LoanRequest>> GetAllUsersLoan(int userId)
        {
            var allLoans = await _loanRequestRepository.GetAllWithFullInfo();
            return allLoans.Where(x => x.UserId == userId);
        }
    }
}

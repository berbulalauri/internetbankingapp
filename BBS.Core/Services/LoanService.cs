using BBS.Core.Helpers;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.Constants;
using BBS.Models.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class LoanService : ILoanService, IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private Account _bankAccount;
        private Card _bankCard;
        private Service _loanService;

        public LoanService(IUnitOfWork unitOfWork, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _bankAccount = _unitOfWork.AccountRepository.GetByName(Constants.BANK_ACCOUNT_Name);
            _bankCard = _unitOfWork.CardRepository.GetCardByName(Constants.BANK_CARD_Name);
            _loanService = _unitOfWork.ServiceRepository.GetServiceByName(Constants.BANK_LOAN_SERVICE_Name);
        }

        public async Task CreateLoan(int loanRequestId)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var loanRequest = await _unitOfWork.LoanRequestRepository.GetByIdAsync(loanRequestId);
                Loan loan = await CreateLoanModel(loanRequestId);

                loanRequest.IsAccepted = true;
                await _unitOfWork.LoanRequestRepository.SaveAsync();

                await _unitOfWork.LoanRepository.InsertAsync(loan);
                await _unitOfWork.LoanRepository.SaveAsync();

                await TransferMoneyToUserAccount(loan, loanRequest);

                await _unitOfWork.CommitAsync();
                _logger.Info($"Loan of {loan.OriginalLoanSum} Created by id: {loan.Id}");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error while sending loan");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task PayLoans()
        {
            var loans = await _unitOfWork.LoanRepository.GetAllWithTrackingAsync();
            var bankAccount = await _unitOfWork.AccountRepository.GetByIdAsync(1);
            Transactions transaction;

            try
            {
                foreach (var loan in loans)
                {
                    await _unitOfWork.BeginTransactionAsync();
                    transaction = await CreateTransactionModel(loan, bankAccount);

                    await TakeMoney(loan.AccountId, loan.MonthlyPayment);
                    await PutMoney(_bankAccount.Id, loan.MonthlyPayment);
                    await _unitOfWork.AccountRepository.SaveAsync();

                    await AddTransactionToHistory(transaction);

                    loan.RemainingLoanSum = CalculateRemainingLoan(loan);
                    await _unitOfWork.LoanRepository.SaveAsync();

                    await _unitOfWork.CommitAsync();
                    _logger.Info($"Pay periodical payment of {loan.MonthlyPayment}, loan id {loan.Id}");
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Error while getting loan money");
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        private async Task TransferMoneyToUserAccount(Loan loan, LoanRequest loanRequest)
        {
            await TakeMoney(_bankAccount.Id, loan.OriginalLoanSum);
            await PutMoney(loanRequest.TransferAccountId, loan.OriginalLoanSum);
            await _unitOfWork.AccountRepository.SaveAsync();

            var transaction = new Transactions
            {
                Amount = loan.OriginalLoanSum,
                Date = DateTime.Now,
                PersonalAccountId = "",
                ReceiverAccountId = loanRequest.TransferAccountId,
                Description = DisplayHelper.DisplayLoanAmountInDescription(loan.OriginalLoanSum),
                SenderAccountId = _bankAccount.Id,
                SenderCardId = _bankCard.Id,
                ServiceId = _loanService.Id
            };

            await AddTransactionToHistory(transaction);
        }

        private async Task PutMoney(int receiverAccountId, decimal amount)
        {
            var result = await _unitOfWork.AccountRepository.GetByIdAsync(receiverAccountId);
            result.Balance += amount;
        }

        private async Task TakeMoney(int senderAccountId, decimal amount)
        {
            var result = await _unitOfWork.AccountRepository.GetByIdAsync(senderAccountId);
            result.Balance -= amount;
        }

        public async Task AddTransactionToHistory(Transactions transactions)
        {
            await _unitOfWork.TransactionsRepository.InsertAsync(transactions);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<Transactions> CreateTransactionModel(Loan loan, Account bankAccount)
        {
            var cards = await _unitOfWork.CardRepository.GetCardsIncludesAsync();
            var card = cards.Where(x => x.AccountId == loan.AccountId).FirstOrDefault();

            return new Transactions
            {
                Amount = loan.MonthlyPayment,
                Date = DateTime.Now,
                PersonalAccountId = "",
                ReceiverAccountId = bankAccount.Id,
                SenderAccountId = loan.AccountId,
                SenderCardId = card.Id,
                ServiceId = _loanService.Id,
                Description = Constants.BANK_LOAN_DESCRIPTION + loan.MonthlyPayment
            };
        }

        private async Task<Loan> CreateLoanModel(int loanRequestId)
        {
            var loanRequest = await _unitOfWork.LoanRequestRepository.GetByIdWithAllInfoAsync(loanRequestId);

            Loan loan = new Loan
            {
                Date = DateTime.Now,
                LoanRequestId = loanRequestId,
                AccountId = loanRequest.AccrueAccountId,
                OriginalLoanSum = loanRequest.LoanSum,
                RemainingLoanSum = loanRequest.LoanSum,
                MonthlyPayment = CalculateTotalMonthlyPayment(loanRequest.LoanSum,
                    loanRequest.LoanType.InterestRate, loanRequest.Term),
                PercentPayment = CalculatePeriodicalInterestPayment(loanRequest.LoanSum,
                    loanRequest.LoanType.InterestRate),
                LoanPayment = CalculateTotalMonthlyPayment(loanRequest.LoanSum,
                    loanRequest.LoanType.InterestRate, loanRequest.Term) -
                    CalculatePeriodicalInterestPayment(loanRequest.LoanSum,
                    loanRequest.LoanType.InterestRate),
                LoanInterestRate = loanRequest.LoanType.InterestRate,
                Status = LoanConstants.DefaultLoanStatus
            };

            return loan;
        }

        private decimal CalculateTotalMonthlyPayment(decimal loanAmount, decimal interestRate,
            int totalNumberOfPayment)
        {
            decimal periodicInterestRate = interestRate / (LoanConstants.DefaultPaymentPerYear * Constants.MAXIMUM_PERCENTAGE);
            decimal constant = (decimal)Math.Pow((1 + (double)periodicInterestRate), totalNumberOfPayment);

            decimal monthlyPayment = loanAmount * (periodicInterestRate * constant) / (constant - 1);

            return (decimal)Math.Round(monthlyPayment, 2);
        }

        private decimal CalculatePeriodicalInterestPayment(decimal remainingLoanSum,
            decimal interestRate)
        {
            var interestInDecimals = interestRate / Constants.MAXIMUM_PERCENTAGE;
            return remainingLoanSum * interestInDecimals / LoanConstants.DefaultPaymentPerYear;
        }

        private decimal CalculateRemainingLoan(Loan loan)
        {
            var remainingLoan = loan.RemainingLoanSum - loan.MonthlyPayment +
                CalculatePeriodicalInterestPayment(loan.RemainingLoanSum, loan.LoanInterestRate);

            if (remainingLoan <= 0)
            {
                remainingLoan = 0;
                loan.Status = Constants.FINISH_LOAN_STATUS;
            }

            return remainingLoan;
        }

        public async void Dispose()
        {
            await _unitOfWork.AccountPropertyRepository.SaveAsync();
        }
    }
}

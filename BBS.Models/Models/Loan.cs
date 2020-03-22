using System;

namespace BBS.Models.Models
{
    public partial class Loan : ISoftDeletable
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal OriginalLoanSum { get; set; }
        public decimal RemainingLoanSum { get; set; }
        public decimal MonthlyPayment { get; set; }
        public decimal PercentPayment { get; set; }
        public decimal LoanInterestRate { get; set; }
        public decimal LoanPayment { get; set; }
        public string Status { get; set; }
        public int LoanRequestId { get; set; }
        public int AccountId { get; set; }

        public virtual LoanRequest LoanRequest { get; set; }
        public virtual Account Account { get; set; }
        public bool IsDeleted { get; set; }
    }
}

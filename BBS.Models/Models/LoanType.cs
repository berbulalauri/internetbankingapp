using BBS.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BBS.Models.Models
{
    public partial class LoanType : ISoftDeletable, IComparable
    {
        public LoanType()
        {
            LoanRequests = new HashSet<LoanRequest>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("General Information")]
        public string GeneralInformation { get; set; }
        [DisplayName("Minimum Loan Sum")]
        public decimal MinLoanSum { get; set; }

        [MaximumAndMinimumValidation("MinLoanSum")]
        [DisplayName("Maximum Loan Sum")]
        public decimal MaxLoanSum { get; set; }
        [DisplayName("Interest Rate")]
        public decimal InterestRate { get; set; }
        [DisplayName("Loan Summary")]
        public string LoanSummary { get; set; }
        [DisplayName("Monthly Fee")]
        public decimal MonthlyFee { get; set; }

        [DisplayName("Minimum Term")]
        public int MinTerm { get; set; }

        [TermMaximumAndMinimumValidation("MinTerm")]
        [DisplayName("Maximu Term")]
        public int MaxTerm { get; set; }
        [DisplayName("Accident Insurance")]
        public decimal? PaymentForAccidentInsurance { get; set; }
        [DisplayName("Insurance Loan Fee")]
        public decimal? FeeForInsuranceLoan { get; set; }
        [DisplayName("Services Upon Receipt")]
        public decimal? FreeForServicesUponReciptCash { get; set; }
        //public DateTime? DebtsRepaymentSchedule { get; set; }
       // public decimal? AdvancedRepayment { get; set; }

        [DisplayName("Currency")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        public virtual ICollection<LoanRequest> LoanRequests { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
        public int CompareTo(object obj)
        {
            return (obj as LoanType).Name.CompareTo(Name);
        }
    }
}

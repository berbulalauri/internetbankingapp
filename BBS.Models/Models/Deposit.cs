using BBS.Models.Constants;
using BBS.Models.CustomValidations;
using System;
using System.ComponentModel.DataAnnotations;


namespace BBS.Models.Models
{
    public partial class Deposit : ISoftDeletable
    {
        public int Id { get; set; }
        public int DepositTypeId { get; set; }
        [Display(Name = ErrorMessages.MaximumDepositMessage)]
        [MoreThanZero]
        [Required(ErrorMessage = ErrorMessages.DepositAmountMessage)]
        public decimal DepositAmount { get; set; }
        public DateTime TermOfDeposit { get; set; } = DateTime.Now;
        public DateTime InterestPayment { get; set; }=DateTime.Now;
        public int CurrencyId { get; set; }
        public int UserId { get; set; }
        public int AccountToTransferId { get; set; }
        public int AccountForInterestId { get; set; }
        public string Status { get; set; }
        public int DepositTermId { get; set; }

        public DepositTerm DepositTerm { get; set; }
        public virtual Account AccountForInterest { get; set; }
        public virtual Account AccountToTransfer { get; set; }
        public virtual Currency Currency { get; set; }
        public virtual DepositType DepositType { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }
    }
}

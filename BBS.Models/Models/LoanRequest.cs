using BBS.Models.CustomValidations;
using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models.Models
{
    public partial class LoanRequest : ISoftDeletable, IComparable
    {
        public int Id { get; set; }
        public int EmployementId { get; set; }

        [Required]
        [Display(Name = "Loan Sum")]
        [MoreThanZero()]
        public decimal LoanSum { get; set; }
        public int LoanTypeId { get; set; }
        [Required]
        public int Term { get; set; }
        public int AccrueAccountId { get; set; }
        public int TransferAccountId { get; set; }
        public int UserId { get; set; }
        public string Comments { get; set; }
        public DateTime DateOfRequest { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsDenied { get; set; }

        public virtual Employment Employment { get; set; }
        public virtual Account AccrueAccount { get; set; }
        public virtual Account TransferAccount { get; set; }
        public virtual User User { get; set; }
        public virtual Loan Loan { get; set; }
        public virtual LoanType LoanType { get; set; }
        public bool IsDeleted { get; set; }
        public int CompareTo(object obj)
        {
            return (obj as LoanRequest).LoanSum.CompareTo(LoanSum);
        }
    }
}

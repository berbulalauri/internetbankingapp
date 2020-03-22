using BBS.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models.Models
{
    public partial class DepositType : ISoftDeletable, IComparable
    {
        public DepositType()
        {
            Deposits = new HashSet<Deposit>();
        }

        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MaxLength(20, ErrorMessage = "Name should NOT exceed 20 characters")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MaxLength(100, ErrorMessage = "Description should NOT exceed 100 characters")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MaxLength(100, ErrorMessage = "Benefit's Description should NOT exceed 100 characters")]
        [Display(Name = "Benefit's Description")]
        public string BenefitsDescription { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MoreThanZero]
        [Display(Name = "Annual Rate")]
        public decimal AnnualRate { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MoreThanZero]
        [Display(Name = "Bonus Rate")]
        public decimal BonusRate { get; set; }

        public int InterestPaymentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MoreThanZero]
        [Display(Name = "Minimum Deposit Amount")]
        public decimal MinimumDepositAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MoreThanZero]
        [Display(Name = "Minimum Replenishment Amount")]
        public decimal MinimumReplenishmentAmount { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} is required")]
        [MoreThanZero]
        [Display(Name = "Maximum Deposit Amount")]
        [MaximumAndMinimumValidation("MinimumDepositAmount")]
        public decimal MaximumDepositAmount { get; set; }
        public int DepositTermId { get; set; }
        public int CurrencyId { get; set; }

        public DepositTerm DepositTerm { get; set; }
        public Currency Currency { get; set; }
        public InterestPaymentType InterestPaymentType { get; set; }
        public virtual ICollection<Deposit> Deposits { get; set; }
        public bool IsDeleted { get; set; }
        public override string ToString()
        {
            return $"{Name}";
        }
        public int CompareTo(object obj)
        {
            return (obj as DepositType).Name.CompareTo(Name);
        }
    }
}

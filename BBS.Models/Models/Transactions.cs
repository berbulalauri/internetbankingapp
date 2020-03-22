using BBS.Models.CustomValidations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using BBS.Models.Constants;


namespace BBS.Models.Models
{
    public partial class Transactions : ISoftDeletable
    {
        public int Id { get; set; }
        public int SenderCardId { get; set; }
        public int SenderAccountId { get; set; }
        public int ReceiverAccountId { get; set; }
        [Required]
        [Display(Name ="Personal Account")]
        [RegularExpression(RegexConstants.RegexForPersonalAccountId,
                   ErrorMessage = ErrorMessages.InvalidPersonalAccountId)]
        public string PersonalAccountId { get; set; }
        public string Description { get; set; }
        [Required]
        [RegularExpression("([1-9][0-9]*[/.]?[0-9]*)", ErrorMessage = "Amount must be a number")]
        [Remote(action: "VerifyAmountValue", controller: "Payment")]
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int ServiceId { get; set; }

        public virtual Account SenderAccount { get; set; }
        public virtual Account ReceiverAccount { get; set; }
        public virtual Card SenderCard { get; set; }
        public virtual Service Service { get; set; }
        public bool IsDeleted { get; set; }
    }
}

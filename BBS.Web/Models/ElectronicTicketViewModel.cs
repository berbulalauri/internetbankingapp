using BBS.Models.Models;
using BBS.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class ElectronicTicketViewModel : Transactions, ISoftDeletable
    {
        [Required]
        [RegularExpression(RegularExpressionPatterns.OnlyNumbers, ErrorMessage = ValidationMessages.OnlyNumbers)]
        [Remote(action: "VerifyAmountValueForElectronicTickets", controller: "Payment")]
        public decimal Amount { get; set; }
    }
}

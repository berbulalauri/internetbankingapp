using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BBS.Models.Constants;

namespace BBS.Web.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings =false)]
        [RegularExpression( RegexConstants.RegexForPhoneNumber, ErrorMessage = ErrorMessages.InvalidNumber)]
        public string Phone { get; set; }
    }
}

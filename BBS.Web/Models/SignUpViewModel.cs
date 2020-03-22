using BBS.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Web.Models
{
    public class SignUpViewModel
    {
        [Required]
        [RegularExpression(RegexConstants.RegexForNames, ErrorMessage = ErrorMessages.InvalidFirstName)]
        public string FirstName { get; set; }
        [Required]
        [RegularExpression(RegexConstants.RegexForNames, ErrorMessage = ErrorMessages.InvalidLastName)]
        public string LatsName { get; set; }
        [Required]
        [RegularExpression(RegexConstants.RegexForPhoneNumber,
                   ErrorMessage = ErrorMessages.InvalidNumber)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

using BBS.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        [RegularExpression(RegexConstants.RegexForPassword, ErrorMessage = ErrorMessages.InvalidPassword)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = ErrorMessages.InvalidConfirmPassword)]
        [RegularExpression(RegexConstants.RegexForPassword, ErrorMessage = ErrorMessages.InvalidPassword)]
        public string ConfirmPassword { get; set; }
    }
}
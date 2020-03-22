using BBS.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class ForgotPasswordConfirmationViewModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }

        

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        [RegularExpression(RegexConstants.RegexForPassword, ErrorMessage ="Password Should contain 1 big letter, 1 small letter, 1 uniqe simbol, 1 number and 8 symbols.")]
       
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Confirm Password")]
        [Compare("NewPassword", ErrorMessage ="Passwordes does not match" )]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}

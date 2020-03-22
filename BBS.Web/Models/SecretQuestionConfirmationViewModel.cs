using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class SecretQuestionConfirmationViewModel
    {
        public int UserId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string SecretQuestion { get; set; }

        [Display(Name = "Answer")]
        public string UserAnswer { get; set; }
    }
}

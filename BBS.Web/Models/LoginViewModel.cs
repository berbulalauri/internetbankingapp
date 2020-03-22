using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string PhoneOrEmail { get; set; }

        public string Identifier { get; set; }

        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

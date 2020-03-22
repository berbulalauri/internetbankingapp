using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Constants
{
    public class RegexConstants
    {
        public const string RegexForNames = @"[A-z ]+[A-z]+";
        public const string RegexForPhoneNumber = @"^[5]+[0-9]{8}";
        public const string RegexForPersonalAccountId = @"^[0-9]{8}";
        public const string RegexForPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$";
        public const string RegExForPassangerName = "^([A-z]+ [A-z]+)";
    }
}

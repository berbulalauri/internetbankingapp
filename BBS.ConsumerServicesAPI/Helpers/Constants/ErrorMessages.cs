using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Helpers.Constants
{
    public class ErrorMessages
    {
        public const string IncorrectPassangerName = "Name can not contain any alphanumeric character and more then one space";
        public const string PassangerNameMinLengthError = "Name should be more then 4 characters long";
        public const string PassangerNameMaxLengthError = "Name should be less then 100 characters long";
    }
}

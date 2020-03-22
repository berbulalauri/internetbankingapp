using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Constants
{
    public class ModelErrors
    {
        public const string NotEnoughAmount = "You dont have enough money to complete this transaction!";
        public const string NotYourCard = "This card does not belong to you!";
        public const string NotExistCard = "This card does not exist in our bank!";
        public const string EmailExists = "Email address already exists";
        public const string PhoneExists = "Phone number already exists";
        public const string PassportIdExists = "Passport ID already exists";
        public const string DestinationError = "Destination city can not be the same as departure city!";
    }
}

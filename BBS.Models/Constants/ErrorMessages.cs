
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Constants
{
    public class ErrorMessages
    {
        public const string InvalidBusId = "Not a valid bus id";
        public const string InvalidApiModel = "Invalid Model";
        public const string InvalidFirstName = "First name is not valid";
        public const string InvalidLastName = "Last name is not valid";
        public const string InvalidNumber = "Entered phone number is not valid!";
        public const string InvalidPersonalAccountId = "Entered personal account id number is not valid";
        public const string InvalidPassword = "Try harder one with capital and lower-case letters and numbers, also use special character(s). With length at least 8!";
        public const string InvalidConfirmPassword = "The new password and confirmation password do not match.";
        public const string InvalidDateMustBeAfter = "'{0}' date must be after '{1}'";
        public const string InvalidPropertyValue = "'{0}' Cannot be equal to '{1}'";
        public const string TransferSuccess = "Money transfered successfully!";
        public const string TransferError = "Error occured while transfering money";
        public const string InvalidAmountValue = "Invalid value for amount!";
        public const string IncorrectPassangerName = "Name can not contain any alphanumeric character and more then one space";
        public const string PassangerNameMinLengthError = "Name should be more then 4 characters long";
        public const string PassangerNameMaxLengthError = "Name should be less then 100 characters long";
        public const string InvalidCardNumber = "Card with this number does not exist";
        public const string MaximumDepositMessage = "Maximum Deposit Amount";
        public const string DepositAmountMessage = "Deposit amount is Empty";
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using BBS.Models.Constants;

namespace BBS.Models.CustomValidations
{
    public sealed class AfterCurrentDate : ValidationAttribute
    {
        private DateTime _currentDate;
        private string _currentDateDisplayName;
        private const string _errorMessage = ErrorMessages.InvalidDateMustBeAfter;

        public AfterCurrentDate() : base(_errorMessage)
        {
            _currentDate = DateTime.Now;
            _currentDateDisplayName = DisplayNames.CurrentDateDisplayName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, _currentDateDisplayName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
        {
            if (_currentDate != null && value != null && Convert.ToDateTime(value) < _currentDate)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}

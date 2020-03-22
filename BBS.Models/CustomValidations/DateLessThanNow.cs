using System;
using System.ComponentModel.DataAnnotations;

namespace BBS.Models.CustomValidations
{
    public class DateLessThanNow : ValidationAttribute
    {
        private const string _errorMessage = "'{0}' must be in Past NOT in FUTURE";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (DateTime)value > DateTime.Now)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name);
        }
    }
}

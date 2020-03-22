using System.ComponentModel.DataAnnotations;

namespace BBS.Models.CustomValidations
{
    public class MoreThanZeroAttribute : ValidationAttribute
    {
        private const string _errorMessage = "'{0}' must be more than zero";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null && (decimal)value <= 0)
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

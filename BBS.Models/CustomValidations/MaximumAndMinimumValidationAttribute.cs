using System.ComponentModel.DataAnnotations;

namespace BBS.Models.CustomValidations
{
    public sealed class MaximumAndMinimumValidationAttribute : ValidationAttribute
    {
        private string _minimum;
        private string _minimumDisplayName;
        private const string _errorMessage = "'{0}' must be greater than '{1}'";

        public MaximumAndMinimumValidationAttribute(string minimum)
            : base(_errorMessage)
        {
            _minimum = minimum;
            _minimumDisplayName = minimum;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, _minimumDisplayName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
                var minimum = validationContext.ObjectType.GetProperty(_minimum);
                var minimumValue = minimum.GetValue(validationContext.ObjectInstance, null);
                if (minimum != null && value != null && (decimal)value < (decimal)minimumValue)
                {
                    _minimumDisplayName = minimum.Name;
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            return null;
        }
    }
}

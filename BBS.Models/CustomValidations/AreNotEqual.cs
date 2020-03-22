using BBS.Models.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Models.CustomValidations
{
    public sealed class AreNotEqual : ValidationAttribute
    {
        private string _currentParameter;
        private string _currentParameterDisplayName;
        private const string _errorMessage = ErrorMessages.InvalidPropertyValue;

        public AreNotEqual(string parameter) : base(_errorMessage)
        {
            _currentParameter = parameter;
            _currentParameterDisplayName = DisplayNames.CurrentIDDisplayName;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, _currentParameterDisplayName);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var current = validationContext.ObjectType.GetProperty(_currentParameter);
            var currentValue = current.GetValue(validationContext.ObjectInstance, null);
            if (current != null && value != null && Convert.ToInt32(value) == Convert.ToInt32(currentValue))
            {
                _currentParameterDisplayName = current.Name;
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}

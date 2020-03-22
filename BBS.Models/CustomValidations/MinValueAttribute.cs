using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Models.CustomValidations
{
   public class MinValueAttribute : ValidationAttribute
    {
        private readonly decimal _val;

        public MinValueAttribute(double val) 
        {
            _val = (decimal)val;
        }
        public string GetErrorMessage() =>
        $"Ammount must be greater then 3!";

        protected override ValidationResult IsValid(object value,
        ValidationContext validationContext)
        {
            if (value == null) return new ValidationResult(" value is null");
            if( Convert.ToDecimal(value) < _val)
            {
                return new ValidationResult(GetErrorMessage());
            }
            return ValidationResult.Success;
        }
    }
 
}

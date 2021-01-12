using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CoffeeShop.Models
{
    public class PositiveNonZeroNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(validationContext.DisplayName);
            var propertyValue = propertyInfo.GetValue(validationContext.ObjectInstance);

            return Convert.ToDouble(propertyValue) > 0 ? ValidationResult.Success : new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }
    }
}
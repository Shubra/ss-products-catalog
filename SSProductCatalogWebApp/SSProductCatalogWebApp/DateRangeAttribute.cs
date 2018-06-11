using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSProductCatalogWebApp
{
  /// <summary>
  /// To validate the LastUpdated which should be a future date.
  /// </summary>
  public class DateRangeAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      DateTime dt = (DateTime)value;
      if (dt < DateTime.UtcNow)
      {
        return ValidationResult.Success;
      }
      return new ValidationResult(ErrorMessage ?? "Please enter the date that is not further from today");
    }
  }
}
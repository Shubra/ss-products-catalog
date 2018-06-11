using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Linq.Dynamic;

namespace SSProductCatalogWebApp {
  [AttributeUsage(AttributeTargets.All, Inherited = false)]
  public class UniqueAttribute : ValidationAttribute {


    public Type TargetModelType { get; set; }
    public string TargetPropertyName { get; set; }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
      return ViewModelValid(value, validationContext);
    }
    
    private ValidationResult ViewModelValid(object value, ValidationContext validationContext) {
      if (validationContext.ObjectType.FullName == "SSProductCatalogWebApp.Models.EditProductViewModels") {
        return ValidationResult.Success;
      }
      else {
        using (ProductCatalogEntities db = new ProductCatalogEntities()) {
          var Name = TargetPropertyName;

          PropertyInfo IdProp = TargetModelType.GetProperties()[1];
          
          var code = validationContext.ObjectInstance.GetType().GetProperty(IdProp.Name).GetValue(validationContext.ObjectInstance, null);

          Type entityType = TargetModelType;
          var result = db.Set(entityType).Where(Name + "==@0", value);

          int count = 0;

          if (code != null) {
            result = result.Where(IdProp.Name + "==@0", code);
          }

          count = result.Count();
          if (count > 0) {
            return new ValidationResult(ErrorMessageString);
          }
          else {
            return ValidationResult.Success;
          }
        }
      }
    }
  }
}
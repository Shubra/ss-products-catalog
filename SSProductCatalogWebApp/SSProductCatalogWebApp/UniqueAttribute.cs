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
      return (TargetModelType == null || string.IsNullOrEmpty(TargetPropertyName)) ? DirectlyValid(value, validationContext) : ViewModelValid(value, validationContext);
    }


    private ValidationResult DirectlyValid(object value, ValidationContext validationContext) {
      using (ProductCatalogEntities db = new ProductCatalogEntities()) {
        var Name = GetName(validationContext);

        PropertyInfo IdProp = validationContext.ObjectInstance.GetType().GetProperties().FirstOrDefault(x => x.CustomAttributes.Count(a => a.AttributeType == typeof(KeyAttribute)) > 0);

        int Id = (int)IdProp.GetValue(validationContext.ObjectInstance, null);

        Type entityType = validationContext.ObjectType;


        var result = db.Set(entityType).Where(Name + "==@0", value);
        int count = 0;

        if (Id > 0) {
          result = result.Where(IdProp.Name + "<>@0", Id);
        }

        count = result.Count();

        if (count == 0)
          return ValidationResult.Success;
        else
          return new ValidationResult(ErrorMessageString);
      }

    }
    private string GetName(ValidationContext validationContext) {
      var Name = validationContext.MemberName;

      if (string.IsNullOrEmpty(Name)) {
        var displayName = validationContext.DisplayName;

        var prop = validationContext.ObjectInstance.GetType().GetProperty(displayName);

        if (prop != null) {
          Name = prop.Name;
        }
        else {
          var props = validationContext.ObjectInstance.GetType().GetProperties().Where(x => x.CustomAttributes.Count(a => a.AttributeType == typeof(DisplayAttribute)) > 0).ToList();

          foreach (PropertyInfo prp in props) {
            var attr = prp.CustomAttributes.FirstOrDefault(p => p.AttributeType == typeof(DisplayAttribute));

            var val = attr.NamedArguments.FirstOrDefault(p => p.MemberName == "Name").TypedValue.Value;

            if (val.Equals(displayName)) {
              Name = prp.Name;
              break;
            }
          }
        }
      }

      return Name;


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
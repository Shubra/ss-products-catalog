using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSProductCatalogWebApp.Models {
  public class ProductViewModels {

    public int Id { get; set; }
    [Required]
    [Unique(ErrorMessage = "This code already exist !!", TargetModelType = typeof(Product), TargetPropertyName = "Code")]
   
    public virtual int Code { get; set; }
    [Required]
    public string Name { get; set; }
    public byte[] Photo { get; set; }
    [Required]
    [Range(1,999,ErrorMessage = "Price should be between 0 and 999")]
    public decimal Price { get; set; }
    [Required]
    [DateRange]
    public System.DateTime LastUpdated { get; set; }
  }
}
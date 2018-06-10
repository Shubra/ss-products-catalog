using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSProductCatalogWebApp.Models {
  public class EditProductViewModels : ProductViewModels {
   
    public override int Code { get; set; }
    

  }
}
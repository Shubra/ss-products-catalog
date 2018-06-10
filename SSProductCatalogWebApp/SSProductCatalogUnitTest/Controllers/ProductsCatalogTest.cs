using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSProductCatalogWebApp.Models;
using System;
using System.Web;

namespace SSProductCatalogWebApp.Controllers.Tests
{
  [TestClass()]
  public class ProductsCatalogTest
  {
    [TestMethod()]
    public void CreateTest()
    {
      var controller = new ProductsCatalogController();
      ProductViewModels productVM = new ProductViewModels()
      {
        Code = 1155,
        Name = "Test prod 1",
        LastUpdated = DateTime.Now,
        Price = 23

      };
      var result = controller.Create(productVM, null);
      Assert.Fail();
    }
  }
}
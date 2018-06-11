using SSProductCatalogWebApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSProductCatalogWebApp.Models;
using System;
using System.Web;
using AutoMapper;
using System.Web.Http;
using System.Web.Http.Results;
using System.Net;

namespace SSProductCatalogWebApp.Controllers.Tests
{
  [TestClass()]
  public class ProductsCatalogTest
  {
    [TestMethod()]
    public void PostProduct()
    {
      var prodApiService = new ProductsAPIService();
      var item = GetDemoProduct();
      prodApiService.PostProduct(item);
      var result = prodApiService.GetProduct(item.Id) as OkNegotiatedContentResult<Product>;

      Assert.IsTrue(result != null);
      Assert.AreEqual(1212, result.Content.Code);
    }

    [TestMethod]
    public void PutProduct_ShouldReturnStatusCode()
    {
      Product product = new Product()
      {
        Id = 4,
        Code = 122,
        Name = "Test prod 1",
        LastUpdated = DateTime.Now,
        Price = 23
      };
      var prodApiService = new ProductsAPIService();
      var result = prodApiService.PutProduct(4, product) as StatusCodeResult;
      
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(StatusCodeResult));
      Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
    }

    /// <summary>
    /// GetProduct should return same code as dummy
    /// </summary>
    [TestMethod]
    public void GetProduct_ShouldReturnProductWithSameID()
    {
      var prodApiService = new ProductsAPIService();
      var item = GetDemoProduct();
      prodApiService.PostProduct(item);
      var result = prodApiService.GetProduct(item.Id) as OkNegotiatedContentResult<Product>;

      Assert.IsNotNull(result);
      Assert.AreEqual(1212, result.Content.Code);
    }

 
    /// <summary>
    /// Delete the product and check for result
    /// </summary>
    [TestMethod]
    public void DeleteProduct_ShouldReturnOK()
    {
      var prodApiService = new ProductsAPIService();
      var item = GetDemoProduct();
      prodApiService.PostProduct(item);
      var result = prodApiService.DeleteProduct(item.Id) as OkNegotiatedContentResult<Product>;

      Assert.IsNotNull(result);
      Assert.AreEqual(1212, result.Content.Code);
    }
    Product GetDemoProduct()
    {
      return new Product() { Id = 3, Code = 1212, Name = "Demo name", Price = 5, LastUpdated = DateTime.Now };
    }
  }
}
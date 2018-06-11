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
    public void PostProduct_ShouldReturnNullSameCodeAsAdded()
    {
      Mapper.Initialize(cfg => { cfg.CreateMap<Product, ProductViewModels>(); cfg.CreateMap<Product, EditProductViewModels>(); });
      var prodApiService = new ProductsAPIService();
      var item = GetDemoProductViewModel();
      var result =  prodApiService.PostProduct(Mapper.Map<Product>(item)) as BadRequestErrorMessageResult;

      Assert.AreSame("Code already exists", "Code already exists");
    }

    [TestMethod]
    public void PutProduct_ShouldReturnErrorForSameCode()
    {
      Product product = GetDemoProductForSameCode();
      var prodApiService = new ProductsAPIService();
      var result = prodApiService.PutProduct(34, product) as BadRequestErrorMessageResult;
      Assert.AreSame("Code already exists", "Code already exists");
    }

    private Product GetDemoProductForSameCode()
    {
      return new Product() { Id = 34, Code = 1212, Name = "Demo name", Price = 5, LastUpdated = DateTime.Now };    }

    /// <summary>
    /// GetProduct should return same code as dummy added
    /// </summary>
    [TestMethod]
    public void GetProduct_ShouldReturnProductWithSameCode()
    {
      var prodApiService = new ProductsAPIService();
      var item = GetDemoProduct();
      var result = prodApiService.GetProduct(item.Id) as OkNegotiatedContentResult<Product>;

      Assert.IsNotNull(result);
      Assert.AreEqual(item.Code, result.Content.Code);
    }

    private ProductViewModels GetDemoProductViewModel()
    {
      return new ProductViewModels() { Id = 35, Code = 2323, Name = "Demo name", Price = 5, LastUpdated = DateTime.Now };
    }


    /// <summary>
    /// Delete the product and check for result
    /// </summary>
    [TestMethod]
    public void DeleteProduct_ShouldReturnOK()
    {
      var prodApiService = new ProductsAPIService();
      var item = GetDeleteDemoProduct();
      prodApiService.PostProduct(item);
      var result = prodApiService.DeleteProduct(item.Id) as OkNegotiatedContentResult<Product>;

      Assert.IsNotNull(result);
      Assert.AreEqual(4040, result.Content.Code);
    }

    private Product GetDeleteDemoProduct()
    {
      return new Product() { Id = new Random(43).Next(44,50), Code = 4040, Name = "Demo name", Price = 55, LastUpdated = DateTime.Now };
    }

    Product GetDemoProduct()
    {
      return new Product() { Id = 34, Code = 1212, Name = "Demo name", Price = 55, LastUpdated = DateTime.Now };
    }
  }
}
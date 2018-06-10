using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using AutoMapper;
using SSProductCatalogWebApp.Models;

namespace SSProductCatalogWebApp.Controllers
{
  public class ProductsCatalogController : Controller
  {
    private ProductsAPIService prodService = new ProductsAPIService();
    // GET: ProductsCatalog
    public ActionResult Index()
    {
      List<ProductViewModels> productVM = Mapper.Map<List<Product>, List<ProductViewModels>>(prodService.GetProducts().ToList());
      return View(productVM);
    }

    [HttpPost]
    public ActionResult Index(string submitbutton, string option, string search)
    {
      try
      {
        switch (submitbutton)
        {
          case "Search":
            return (Search(option, search));
          case "Export to Excel":
            return (ExportToExcel());
          case "Clear Search":
            return (Index());
          default:
            return View();
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Action not found" + ex.Message, ex);
      }
    }

    /// <summary>
    /// Searching through the Product List based on Code, Name or Price
    /// </summary>
    /// <param name="option"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public ActionResult Search(string option, string search)
    {
      List<ProductViewModels> productVM = Mapper.Map<List<Product>, List<ProductViewModels>>(prodService.GetProducts().ToList());
      if (option == "Code")
      {
        return View(productVM.Where(a => a.Code == Convert.ToInt32(search) || search == null));
      }
      if (option == "Name")
      {
        return View(productVM.Where(a => a.Name.Contains(search) || search == null));
      }
      if (option == "Price")
      {
        return View(productVM.Where(a => a.Price == Convert.ToInt32(search) || search == null));
      }
      return View(productVM);
    }


    // GET: ProductsCatalog/Details/5
    public ActionResult Details(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductViewModels productVM = id.HasValue ? Mapper.Map<ProductViewModels>(prodService.GetProductById(id.Value)) : null;
      if (productVM == null)
      {
        return HttpNotFound();
      }
      return View(productVM);
    }

    // GET: ProductsCatalog/Create
    public ActionResult Create()
    {
      return View();
    }

    // POST: ProductsCatalog/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,Code,Name,Photo,Price,LastUpdated")] ProductViewModels productVM, HttpPostedFileBase file)
    {

      if (ModelState.IsValid)
      {
        productVM.Photo = file != null ? UploadAndSaveImage(productVM.Photo, file) : null;

        prodService.PostProduct(Mapper.Map<ProductViewModels, Product>(productVM));
        return RedirectToAction("Index");

      }


      return View(productVM);
    }

    /// <summary>
    /// Upload the image to app_data/photos folder and then save it to Database
    /// </summary>
    /// <param name="productVM"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    private byte[] UploadAndSaveImage(byte[] photo, HttpPostedFileBase file)
    {
      try
      {
        if (file != null && file.ContentLength > 0)
        {
          var fileName = Path.GetFileName(file.FileName);
          // store the file inside ~/App_Data/uploads folder
          var path = Path.Combine(Server.MapPath("~/App_Data/Photos"), fileName);
          file.SaveAs(path);

          photo = new byte[file.ContentLength];
          file.InputStream.Read(photo, 0, file.ContentLength);
          return photo;
        }
        return null;
      }
      catch (Exception ex)
      {
        throw new Exception("Something went wrong while saving image" + ex.Message, ex);
      }
    }

    // GET: ProductsCatalog/Edit/5
    public ActionResult Edit(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      Product product = id.HasValue ? prodService.GetProductById(id.Value) : null;
      if (product == null)
      {
        return HttpNotFound();
      }
      return View(Mapper.Map<EditProductViewModels>(product));
    }

    // POST: ProductsCatalog/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,Code,Name,Photo,Price,LastUpdated")] EditProductViewModels editProductViewModel, HttpPostedFileBase file)
    {
      if (ModelState.IsValid)
      {
        editProductViewModel.Photo = file != null ? UploadAndSaveImage(editProductViewModel.Photo, file) : null;

        prodService.PutProduct(editProductViewModel.Id, Mapper.Map<EditProductViewModels, Product>(editProductViewModel));

        return RedirectToAction("Index");
      }
      return View(editProductViewModel);
    }

    // GET: ProductsCatalog/Delete/5
    public ActionResult Delete(int? id)
    {
      if (id == null)
      {
        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
      }
      ProductViewModels productVM = id.HasValue ? Mapper.Map<ProductViewModels>(prodService.GetProductById(id.Value)) : null;
      if (productVM == null)
      {
        return HttpNotFound();
      }
      return View(productVM);
    }

    // POST: ProductsCatalog/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
      Product product = prodService.GetProductById(id);
      prodService.DeleteProduct(id);
      return RedirectToAction("Index");
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
      {
        prodService.Dispose();
      }
      base.Dispose(disposing);
    }

    /// <summary>
    /// Export the product list to excel
    /// </summary>
    /// <returns></returns>db
    [HttpPost]
    public ActionResult ExportToExcel()
    {
      List<ProductViewModels> productVM = Mapper.Map<List<Product>, List<ProductViewModels>>(prodService.GetProducts().ToList());
      var gvExcel = new GridView();
      gvExcel.DataSource = productVM;
      gvExcel.DataBind();
      Response.ClearContent();
      Response.Buffer = true;
      Response.AddHeader("content-disposition", "attachment; filename=ProductExcel.xls");
      Response.ContentType = "application/ms-excel";
      Response.Charset = "";
      StringWriter objStringWriter = new StringWriter();
      HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
      gvExcel.RenderControl(objHtmlTextWriter);
      Response.Output.Write(objStringWriter.ToString());
      Response.Flush();
      Response.End();
      return View("Index", productVM);
    }

  }
}

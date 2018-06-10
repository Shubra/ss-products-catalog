using AutoMapper;
using Microsoft.Owin;
using Owin;
using SSProductCatalogWebApp.Models;

[assembly: OwinStartupAttribute(typeof(SSProductCatalogWebApp.Startup))]
namespace SSProductCatalogWebApp {
  public partial class Startup {
    public void Configuration(IAppBuilder app) {
      ConfigureAuth(app);
    }
  }
}

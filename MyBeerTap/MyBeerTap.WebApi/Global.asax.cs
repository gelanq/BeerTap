using System.Data.Linq.Mapping;
using System.Web.Http;
using MyBeerTap.WebApi.Infrastructure;
using System.Data.Entity;
using System.Web.UI;
using MyBeerTap.Model;
using MyBeerTap.Model.Data;

namespace MyBeerTap.WebApi
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
             Database.SetInitializer(new BeerTapDBContextSeeder());
            BootStrapper.Initialize(GlobalConfiguration.Configuration);
            
        }
    }
}
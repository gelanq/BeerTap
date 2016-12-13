using System.Web.Http.Description;
using System.Web.Mvc;
using System.Web.Http;
using IQ.Platform.Framework.WebApi.HelpGen;
using IQ.Platform.Framework.WebApi.Infrastructure;
using IQ.Platform.Framework.WebApi.Reflection;


namespace MyBeerTap.Documentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HelpController : HelpControllerBase
    {

        public HelpController(
            HttpConfiguration config, IiQApiExplorer apiExplorer, IControllerTypeResolver typeResolver,
            IApiBaseAddressPathProvider basePathProvider)
            : base(config, apiExplorer, typeResolver, basePathProvider)
        {
        }


        public ActionResult Index()
        {

            var apiDescriptions = GetIndexPageModel();

            ViewResult viewResult = View(apiDescriptions);
            viewResult.ViewBag.Title = string.Format("{0} Help Page", _apiName.Value);
            return viewResult;

        }

        public ActionResult Api(string apiId)
        {

            var apiModel = GetApiPageModel(apiId);

            return apiModel != null
                       ? View(apiModel)
                       : View("Error");
        }

    }
}

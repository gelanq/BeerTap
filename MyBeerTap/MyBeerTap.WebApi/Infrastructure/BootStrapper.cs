using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;
using Castle.Windsor;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.AspNet.Infrastructure;
using IQ.Platform.Framework.WebApi.HelpGen;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.ApiServices;
using MyBeerTap.Documentation.Controllers;
using IQ.Platform.Framework.WebApi.Infrastructure;

namespace MyBeerTap.WebApi.Infrastructure
{
    public static class BootStrapper
    {

        public static void Initialize(
            HttpConfiguration config,
            IApiContainer container = null,
            IDomainServiceResolver domainServiceResolver = null,
            Action<HttpConfiguration> postConfigure = null,
            bool initializeHelpPage = true) // TODO: refactor to instance BootStrapper class and make InitializeDocumentationApplication() public; pass all parameter in a constructor or factory
        {

            if (config == null)
                throw new ArgumentNullException("config");

            var hconfig = SetConfiguration(config, postConfigure);

            container = container ?? CreateContainer(config, domainServiceResolver);
            container.RegisterDependencies();
            SetupDependencyResolver(config, container);

            ConfigureFormatters(config, container);
            ConfigureMessageHandlers(config.MessageHandlers, container);
            ConfigureFilters(config.Filters, container);

            container.Resolve<IWebApiConfigurator>()
                    .Configure(config, hconfig);

            if (initializeHelpPage)
                InitializeDocumentationApplication(config, container);

        }

        static void InitializeDocumentationApplication(HttpConfiguration config, IApiContainer container)
        {

            const string apiName = "MyBeerTap API documentation";

            // initialize help documentation module
            ApiDocumentationApplication.Initialize(
                container,
                config,
                new ApiDocumentationConfiguration(
                    container.ResourceAssembly,
                    typeof(HelpController).Assembly,
                    () => apiName)
                );
        }

        static IApiContainer CreateContainer(HttpConfiguration configuration, IDomainServiceResolver domainServiceResolver)
        {
            var windsorContainer = new WindsorContainer();
            return new DefaultApiContainer(configuration, windsorContainer, domainServiceResolver);
        }

        static HypermediaConfiguration SetConfiguration(HttpConfiguration config, Action<HttpConfiguration> postConfigure)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            // prevents checking error detail policy that fails (for IncludeErrorDetailPolicy.Default)
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            var hconfig = new HypermediaConfiguration
            {
                EntryPoint = { ControllerType = typeof(EntryPointController<ApiEntryPointResource>), },
                //ApiRelativePath = new Uri("internalPath", UriKind.Relative),
                PingResourceFactory = typeof(CustomPingResourceFactory)
            };

            // TODO: if required the eTag and cache-control headers can be disabled
            //config.SetCaching(HttpCacheControlConfiguration.Disabled);

            config.SetHypermediaConfiguration(hconfig);

            // run additional configuration
            (postConfigure ?? (c => { /*do nothing by default*/ }))(config);

            return hconfig;
        }

        static void ConfigureFormatters(HttpConfiguration config, IApiContainer container)
        {

            // Remove all the default formatters
            config.Formatters.Clear();

            // Add all the formatters resolved by the container
            container.ResolveMediaTypeFormatters().Apply(config.Formatters.Add);

            //// Add the custom formatters
            //config.Formatters.Add(container.ResolveMediaTypeFormatter<HalJsonMediaTypeFormatter>());

        }

        static void ConfigureMessageHandlers(ICollection<DelegatingHandler> messageHandlers, IApiContainer container)
        {
            // TODO: Add handlers for implementing security
            // NOTE: The message handlers included in this template are RQ specific 
            //       and may need to be altered to meet the security requirements of a new API.

            container.ResolveMessageHandlers().Apply(messageHandlers.Add);
        }

        static void ConfigureFilters(HttpFilterCollection filters, IApiContainer container)
        {
            container
                .ResolveExceptionFilters()
                .Apply(filters.Add);
        }

        static void SetupDependencyResolver(HttpConfiguration config, IApiContainer container)
        {
            // REST Api resolver
            config.DependencyResolver = container;
        }

    }

}

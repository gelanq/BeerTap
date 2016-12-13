using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.Logging;
using MyBeerTap.ApiServices;
using MyBeerTap.Services;

namespace MyBeerTap.WebApi.Infrastructure.Installers
{

    /// <summary>
    /// Registers domain specific services using a service resolver if provided, otherwise register all services from provided assembly.
    /// </summary>
    public class DomainServicesInstaller : IWindsorInstaller
    {
        readonly IDomainServiceResolver _customDomainServiceResolver;
        readonly Assembly _apiDomainServicesAssembly;
        readonly Assembly _apiDomainServiceInterfacesAssembly;

        public DomainServicesInstaller(IDomainServiceResolver customDomainServiceResolver, Assembly apiDomainServicesAssembly,
                                       Assembly apiDomainServiceInterfacesAssembly)
        {
            if (apiDomainServicesAssembly == null)
                throw new ArgumentNullException("apiDomainServicesAssembly");
            if (apiDomainServiceInterfacesAssembly == null)
                throw new ArgumentNullException("apiDomainServiceInterfacesAssembly");

            _customDomainServiceResolver = customDomainServiceResolver;
            _apiDomainServicesAssembly = apiDomainServicesAssembly;
            _apiDomainServiceInterfacesAssembly = apiDomainServiceInterfacesAssembly;
        }


        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            if (_customDomainServiceResolver != null)
                RegisterUsingTheResolver(container);

            else
                RegisterUsingContainer(container);
        }

        void RegisterUsingTheResolver(IWindsorContainer container)
        {

            // register each domain service to be resolved using provided resolver
            DomainServiceInterfaces
                .Apply(domainService => container
                                            .Register(Component.For(domainService)
                                                               .UsingFactoryMethod(
                                                                   (kelner, context) =>
                                                                   _customDomainServiceResolver.Resolve(context.RequestedType)))
                );
        }

        void RegisterUsingContainer(IWindsorContainer container)
        {

            container
                .Register(Classes.FromAssembly(_apiDomainServicesAssembly).BasedOn<IDomainService>()
                 .Configure(
                                    cr =>
                                    {
                                        var x =
                                            cr.Interceptors(InterceptorReference.ForKey(LoggingConstants.DomainServiceLogger)).Anywhere;
                                    })
                .WithServiceFromInterface());

        }

        IEnumerable<Type> DomainServiceInterfaces
        {
            get
            {
                return
                    _apiDomainServiceInterfacesAssembly
                        .GetTypes()
                        .Where(t => t.IsInterface && typeof(IDomainService).IsAssignableFrom(t));
            }
        }
    }
}

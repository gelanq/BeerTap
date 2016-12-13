using System;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using IQ.Platform.Framework.Common.Mapping;
using IQ.Platform.Framework.WebApi.Services.Mapping;

namespace MyBeerTap.WebApi.Infrastructure.Installers
{
    public class ResourceMapperInstaller : IWindsorInstaller
    {
        readonly Assembly _resourceMappersAssembly;

        public ResourceMapperInstaller(Assembly resourceMappersAssembly)
        {
            if (resourceMappersAssembly == null)
                throw new ArgumentNullException("resourceMappersAssembly");
            _resourceMappersAssembly = resourceMappersAssembly;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // register resource mappers

            var apiMappersAssemblyDesc = Classes.FromAssembly(_resourceMappersAssembly);

            container
                   .Register(apiMappersAssemblyDesc.BasedOn(typeof(IMapper<,>)).WithServiceAllInterfaces())
                   .Register(Component.For<IMapperFactory>().ImplementedBy<ReflectionBasedMapperFactory>())
                  ;

            // register 
            //container.Register(apiMappersAssemblyDesc.BasedOn(typeof(IApiApplicationService<,>)).WithServiceAllInterfaces());
        }
    }
}

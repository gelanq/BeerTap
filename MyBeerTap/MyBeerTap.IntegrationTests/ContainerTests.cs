using System;
using System.Linq;
using System.Web.Http;
using Castle.Windsor;
using FluentAssertions;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Helpers;
using IQ.Platform.Framework.WebApi.Infrastructure;
using IQ.Platform.Framework.WebApi.Reflection;
using Ploeh.AutoFixture;
using MyBeerTap.WebApi.Infrastructure;
using MyBeerTap.Model;
using Xunit;

namespace MyBeerTap.IntegrationTests
{
    public class ContainerTests
    {

        #region customizations

        class WindsorContainerCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Register(() =>
                {
                    var config = new HttpConfiguration();
                    IWindsorContainer windsorContainer = new WindsorContainer();
                    BootStrapper.Initialize(config, new DefaultApiContainer(config, windsorContainer), initializeHelpPage: false);
                    return windsorContainer;
                });
            }
        }

        #endregion

        [Fact]
        public void ItShouldResolveGenericApiControllerForEachResourceFound()
        {

            Type[] excludedResources =
            {
            };

            var fixture = new Fixture().Customize(new WindsorContainerCustomization());
            var container = fixture.Create<IWindsorContainer>();

            var resourcesAssembly = typeof(LinkRelations).Assembly;
            var helper = new ResourceRelatedGenericTypesResolver(new TypesHelper(), resourcesAssembly);
            var allResources = ResourceRelatedGenericTypesResolver.GetDefaultResourceTypesSelector(resourcesAssembly)();

            // resolve api controller for each found resource
            allResources
                .Except(excludedResources)
                .Apply(r =>
                {
                    var desc = helper.ResolveResourceTypeInfo(r);
                    var controllerType = helper.MakeGenericTypeForResource(TypesHelper.GenericApiControllerInterfaceType, desc);

                    try
                    {

                        // using reflection, calls: windsorContainer.Resolve<IGenericApiController<Resource, State, Key>>()
                        CallResolverFor(controllerType, container);

                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Cannot resolve api controller for {0} resource.", desc.ResourceType.Name), ex);
                    }

                });

            // the test pass if get here

        }

        static object CallResolverFor(Type typeToResolve, IWindsorContainer container)
        {
            return ReflectionHelper.InvokeGenericMethod<object>(container, "Resolve", new[] { typeToResolve });
        }


        public class TestEntryPointController : ApiController, IEntryPointRequestHandler { }

        [Fact]
        public void ItShouldResolveEntryPointControllerSetInConfiguration()
        {

            // arrange
            var config = new HttpConfiguration();
            var sut = new DefaultApiContainer(config, new WindsorContainer());
            BootStrapper.Initialize(config, sut, null, c => c.GetHypermediaConfiguration().EntryPoint.ControllerType = typeof(TestEntryPointController), initializeHelpPage: false);

            // act
            var result = sut.Resolve<IEntryPointRequestHandler>();

            // assert
            result.Should().BeOfType<TestEntryPointController>();

        }

    }
}

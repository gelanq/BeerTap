using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.AspNet.Handlers;
using IQ.Platform.Framework.WebApi.Services.Security;
using MyBeerTap.ApiServices.Security;

namespace MyBeerTap.WebApi.Handlers
{
    public class PutYourApiSafeNameUserContextProvidingHandler
            : ApiSecurityContextProvidingHandler<MyBeerTapApiUser, NullUserContext>
    {

        public PutYourApiSafeNameUserContextProvidingHandler(
            IStoreDataInHttpRequest<MyBeerTapApiUser> apiUserInRequestStore)
            : base(new MyBeerTapUserFactory(), CreateContextProvider(), apiUserInRequestStore)
        {
        }

        static ApiUserContextProvider<MyBeerTapApiUser, NullUserContext> CreateContextProvider()
        {
            return
                new ApiUserContextProvider<MyBeerTapApiUser, NullUserContext>(_ => new NullUserContext());
        }
    }
}
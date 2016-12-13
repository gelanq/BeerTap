using System.Net.Http;
using IQ.Platform.Framework.WebApi.Security;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace MyBeerTap.WebApi.Security
{
    //TODO: comment out below class to turn on SSO based authentication
    public class AlwaysAuthenticateRequestAuthenticator : IRequestAuthenticator<UserAuthData>
    {
        public UserAuthData Verify(HttpRequestMessage request)
        {
            return new UserAuthData("null token");
        }
    }
}

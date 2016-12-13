using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.AspNet;
using IQ.Platform.Framework.WebApi.Services.Security;

namespace MyBeerTap.ApiServices.Security
{

    public class MyBeerTapApiUser : ApiUser<UserAuthData>
    {
        public MyBeerTapApiUser(string name, Option<UserAuthData> authData)
            : base(authData)
        {
            Name = name;
        }

        public string Name { get; private set; }

    }

    public class MyBeerTapUserFactory : ApiUserFactory<MyBeerTapApiUser, UserAuthData>
    {
        public MyBeerTapUserFactory() :
            base(new HttpRequestDataStore<UserAuthData>())
        {
        }

        protected override MyBeerTapApiUser CreateUser(Option<UserAuthData> auth)
        {
            return new MyBeerTapApiUser("MyBeerTap user", auth);
        }
    }

}

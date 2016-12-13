 
using IQ.Platform.Framework.WebApi;
using MyBeerTap.Model;

namespace MyBeerTap.ApiServices
{
    public interface IOfficeApiService : IGetAResourceAsync<Office, int>,
        IGetManyOfAResourceAsync<Office, int>,
        ICreateAResourceAsync<Office, int>,
        IUpdateAResourceAsync<Office, int>,
        IDeleteResourceAsync<Office, int>
    {
    }
}

 
using IQ.Platform.Framework.WebApi;
using MyBeerTap.Model;

namespace MyBeerTap.ApiServices
{
   public interface IKegReplaceApiService : ICreateAResourceAsync<ReplaceKeg, int>,
        IUpdateAResourceAsync<ReplaceKeg, int>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;
using MyBeerTap.ApiServices.Security;
using MyBeerTap.Model;
using MyBeerTap.Model.Data;

namespace MyBeerTap.ApiServices
{
   public class KegReplaceApiService: IKegReplaceApiService
    {
        readonly IApiUserProvider<MyBeerTapApiUser> _userProvider;
        private BeeerTapRepository _repository;



        public KegReplaceApiService(IApiUserProvider<MyBeerTapApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

        }

 

        public Task<ResourceCreationResult<ReplaceKeg, int>> CreateAsync(ReplaceKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();

            Keg k;
            var tapId = context.UriParameters.GetByName<int>("TapId").EnsureValue(() => context.CreateHttpResponseException<Tap>("The TapId must be supplied in the URI", HttpStatusCode.BadRequest));
            Tap t = _repository.GetTapById(tapId);

            //check if Tap exist.
            if (t != null)
            {

                k = _repository.ReplaceKeg(resource.Keg);
            }
            else
                throw new Exception("Tap doesn't exist. Cannot replace keg.");


            return Task.FromResult(new ResourceCreationResult<ReplaceKeg, int>(resource));
        }

        public Task<ReplaceKeg> UpdateAsync(ReplaceKeg resource, IRequestContext context, CancellationToken cancellation)
        {
            //_repository = new BeeerTapRepository();
            //Keg k;
            //var tapId = context.UriParameters.GetByName<int>("Id").EnsureValue(() => context.CreateHttpResponseException<Tap>("The TapId must be supplied in the URI", HttpStatusCode.BadRequest));
            //Tap t = _repository.GetTapById(tapId);

            ////check if Tap exist.
            //if (t != null)
            //{

            //    k = _repository.ReplaceKeg(resource.Keg);
            //}
            //else
            //    throw new Exception("Tap doesn't exist. Cannot replace keg.");

            return Task.FromResult( resource);
        }

      
    }
}

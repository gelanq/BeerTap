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
    public class GlassApiService : IGlassApiService
    {
        readonly IApiUserProvider<MyBeerTapApiUser> _userProvider;
        private BeeerTapRepository _repository;



        public GlassApiService(IApiUserProvider<MyBeerTapApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

        }

        public Task<ResourceCreationResult<Glass, int>> CreateAsync(Glass resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();

            var tapId = context.UriParameters.GetByName<int>("TapId").EnsureValue(() => context.CreateHttpResponseException<Tap>("The TapId must be supplied in the URI", HttpStatusCode.BadRequest));

            Keg k = _repository.GetKegByTapId(tapId);
            if (k.Remaining < resource.AmountToPour)
               throw new Exception("Not enough beer in this Tap!!!!!");

            //Add new Glass
            Glass g = new Glass();
            resource.TapId = tapId;
            g = _repository.AddGlass(resource);
            //Update Keg
            _repository.UpdateKegByGlass(g);
            
            return Task.FromResult(new ResourceCreationResult<Glass, int>(g));
            
        }

 

      


    }
}

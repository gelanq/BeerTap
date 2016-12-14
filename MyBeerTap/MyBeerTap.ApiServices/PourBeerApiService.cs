using System;
using System.Net;
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
    public class PourBeerApiService: IPourBeerApiService
    {
        readonly IApiUserProvider<MyBeerTapApiUser> _userProvider;
        private BeeerTapRepository _repository;
         

        public PourBeerApiService(IApiUserProvider<MyBeerTapApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;

        }
        public Task<ResourceCreationResult<PourBeer, int>> CreateAsync(PourBeer resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();

            var tapId = context.UriParameters.GetByName<int>("TapId").EnsureValue(() => context.CreateHttpResponseException<Tap>("The TapId must be supplied in the URI", HttpStatusCode.BadRequest));

            Keg k = _repository.GetKegByTapId(tapId);
            if (k.Remaining < resource.Glass.AmountToPour)
                throw new Exception("Not enough beer in this Tap!!!!!");

            //Add new Glass
            resource.Id = tapId;
            resource.Glass.TapId = tapId;
            Glass g = _repository.AddGlass(resource.Glass);

            //Update the Keg
            _repository.UpdateKegByGlass(g);


            //Get the Tap for reference
            Tap t = _repository.GetTapById(tapId);
            resource.Tap = t;


            return Task.FromResult(new ResourceCreationResult<PourBeer, int>(resource));
        }

    }
}

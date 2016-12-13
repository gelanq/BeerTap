using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using MyBeerTap.Model;
using MyBeerTap.Model.Data;

namespace MyBeerTap.ApiServices
{
   public class TapApiService: ITapApiService
   {
       private BeeerTapRepository _repository;
        public Task<Tap> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {

            _repository = new BeeerTapRepository();

            return Task.FromResult(_repository.GetTapById(id));
           
        }

        public Task<IEnumerable<Tap>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            var officeId = context.UriParameters.GetByName<int>("OfficeId").EnsureValue(() => context.CreateHttpResponseException<Tap>("The officeId must be supplied in the URI", HttpStatusCode.BadRequest));
            _repository = new BeeerTapRepository();

            return Task.FromResult(_repository.GetTaps(officeId));
        }


        public Task<Tap> UpdateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();

            return   Task.FromResult(_repository.UpdateTap(resource));

      
        }


        public Task<ResourceCreationResult<Tap, int>> CreateAsync(Tap resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();

            Keg keg = resource.Keg;
          //  resource.Keg = null;

            Tap tap = resource;

              tap = _repository.AddTap(resource);

            if (keg != null)
            {
                keg.TapId  = tap.Id;
                _repository.ReplaceKeg(resource.Keg);
            }


            return Task.FromResult(new ResourceCreationResult<Tap, int>(resource));
        }

      
        public Task DeleteAsync(ResourceOrIdentifier<Tap, int> input, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();
            _repository.RemoveTap(input.Resource);
            return Task.FromResult<Tap>(null);

        }


    }
}

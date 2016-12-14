
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using IQ.Foundation.Utilities;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi;
using IQ.Platform.Framework.WebApi.Services.Security;
using MyBeerTap.ApiServices.Security;

using MyBeerTap.Model;
using MyBeerTap.Model.Data;


namespace MyBeerTap.ApiServices
{
    public class OfficeApiService: IOfficeApiService
    {
        readonly IApiUserProvider<MyBeerTapApiUser> _userProvider;
        private BeeerTapRepository _repository;
  


        public OfficeApiService(IApiUserProvider<MyBeerTapApiUser> userProvider)
        {
            if (userProvider == null)
                throw new ArgumentNullException("userProvider");
            _userProvider = userProvider;
          
        }


        public Task<Office> GetAsync(int id, IRequestContext context, CancellationToken cancellation)
        {

            _repository = new BeeerTapRepository();
          
            return Task.FromResult(_repository.GetOfficeById(id));

            
        }

        public Task<IEnumerable<Office>> GetManyAsync(IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();
            return Task.FromResult(_repository.GetOffices());

             
        }

        public Task<ResourceCreationResult<Office, int>> CreateAsync(Office resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();
            Office office = _repository.AddOffice(resource);
 

            return Task.FromResult(new ResourceCreationResult<Office, int>(resource));
        }

        public Task<Office> UpdateAsync(Office resource, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();
            return Task.FromResult( _repository.UpdateOffice(resource));
        }

        public Task DeleteAsync(ResourceOrIdentifier<Office, int> input, IRequestContext context, CancellationToken cancellation)
        {
            _repository = new BeeerTapRepository();
            _repository.RemoveOffice(input.Resource);
            return Task.FromResult<Office>(null);
            
        }

    }
}

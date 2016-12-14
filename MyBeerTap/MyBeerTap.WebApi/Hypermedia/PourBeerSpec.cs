 
using System.Collections.Generic;
 
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi.Hypermedia
{
    public class PourBeerSpec : SingleStateResourceSpec<PourBeer, int>
    {
        public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({TapId})/PourBeer");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Taps.PourBeer; }
        }
        protected override IEnumerable<ResourceLinkTemplate<PourBeer>> Links()
        {

            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapAtOffice, r => r.OfficeId, r => r.Id);
        }
        public override IResourceStateSpec<PourBeer, NullState, int> StateSpec
        {
            get
            {
                return
                  new SingleStateSpec<PourBeer, int>
                  {
                      Links =
                      {
                           CreateLinkTemplate(LinkRelations.Tap , TapSpec.UriTapAtOffice , r=> r.OfficeId, r => r.Id),
                      },
                      Operations = new StateSpecOperationsSource<PourBeer, int>
                      {

                          InitialPost = ServiceOperations.Create,
                          Post = ServiceOperations.Update,
                          Put = ServiceOperations.Update,
                      },
                  };
            }
        }
    }
}
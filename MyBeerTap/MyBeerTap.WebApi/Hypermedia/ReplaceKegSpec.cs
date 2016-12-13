using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi.Hypermedia
{
    public class ReplaceKegSpec : SingleStateResourceSpec<ReplaceKeg, int>
    {

        public static ResourceUriTemplate UriTapAtOffice = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({TapId})/ReplaceKeg");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Taps.ReplaceKeg; }
        }
        protected override IEnumerable<ResourceLinkTemplate<ReplaceKeg>> Links()
        {

            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapAtOffice, r => r.OfficeId, r => r.Id);
        }
        public override IResourceStateSpec<ReplaceKeg, NullState, int> StateSpec
        {
            get
            {
                return
                  new SingleStateSpec<ReplaceKeg, int>
                  {
                      Links =
                      {
                           CreateLinkTemplate(LinkRelations.Tap , TapSpec.UriTapAtOffice , r=> r.OfficeId, r => r.Id),
                      },
                      Operations = new StateSpecOperationsSource<ReplaceKeg, int>
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
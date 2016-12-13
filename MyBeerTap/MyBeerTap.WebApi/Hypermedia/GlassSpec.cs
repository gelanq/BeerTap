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
    public class GlassSpec : SingleStateResourceSpec<Glass, int>
    {

        public static ResourceUriTemplate UriGlassAtTap = ResourceUriTemplate.Create("Taps({TapId})/Glass");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Glass; }
        }

        protected override IEnumerable<ResourceLinkTemplate<Glass>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriGlassAtTap, c => c.TapId);
        }

        public override IResourceStateSpec<Glass, NullState, int> StateSpec
        {
            get
            {
                return
                  new SingleStateSpec<Glass, int>
                  {
                      Links =
                      {
                           CreateLinkTemplate(LinkRelations.Tap,  UriGlassAtTap, r => r.TapId  )
                      },
                      Operations = new StateSpecOperationsSource<Glass, int>
                      {
                          Get = ServiceOperations.Get,
                          InitialPost = ServiceOperations.Create,
                          Post = ServiceOperations.Update,
                          Delete = ServiceOperations.Delete,
                      },
                  };
            }
        }
    }
}
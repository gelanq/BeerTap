
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi.Hypermedia
{
    public class OfficeSpec: SingleStateResourceSpec<Office, int>
    {

        public static ResourceUriTemplate Uri = ResourceUriTemplate.Create("Offices({id})");
       

        public override string EntrypointRelation
        {
            get { return LinkRelations.Office; }
        }

        public override IResourceStateSpec<Office, NullState, int> StateSpec
        {
            get
            {
                return
                  new SingleStateSpec<Office, int>
                  {
                      Links =
                      {
                           CreateLinkTemplate(LinkRelations.Taps.Plurl , TapSpec.UriTapAtOffice.Many , r => r.Id),
                      },
                      Operations = new StateSpecOperationsSource<Office, int>
                      {
                          Get = ServiceOperations.Get,
                          InitialPost = ServiceOperations.Create,
                          Post = ServiceOperations.Update,
                          Put = ServiceOperations.Update,
                          Delete = ServiceOperations.Delete,
                      },
                  };
            }
        }

    }
}
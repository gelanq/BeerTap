
using System.Collections.Generic;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi.Hypermedia
{
    public class KegSpec : SingleStateResourceSpec<Keg, int>
    {

        public static ResourceUriTemplate UriKegAtTap = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({TapId})/Keg");


        public override string EntrypointRelation
        {
            get { return LinkRelations.Keg; }
        }

        protected override IEnumerable<ResourceLinkTemplate<Keg>> Links()
        {

            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriKegAtTap, c => c.TapId);
        }

        public override IResourceStateSpec<Keg, NullState, int> StateSpec
        {
            get
            {
                return
                    new SingleStateSpec<Keg, int>
                    {
                        Links =
                        {
                            CreateLinkTemplate(LinkRelations.Tap,  UriKegAtTap, r=> r.TapId ),
                          

                        },
                        Operations = new StateSpecOperationsSource<Keg, int>
                        {

                            InitialPost = ServiceOperations.Create,

                        },
                    };
            }
        }

    }
}
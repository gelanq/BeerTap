 
using System.Collections.Generic;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Hypermedia.Specs;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi.Hypermedia
{
    public class TapSpec : ResourceSpec<Tap, KegState, int>
    {
        public static ResourceUriTemplate UriTapAtOffice  = ResourceUriTemplate.Create("Offices({OfficeId})/Taps({Id})");

        public override string EntrypointRelation
        {
            get { return LinkRelations.Tap; }
        }
        protected override IEnumerable<ResourceLinkTemplate<Tap>> Links()
        {
            yield return CreateLinkTemplate(CommonLinkRelations.Self, UriTapAtOffice, c => c.OfficeId, c => c.Id);
        }
        protected override IEnumerable<IResourceStateSpec<Tap, KegState, int>> GetStateSpecs()
        {
            //yield return new ResourceStateSpec<Tap, KegState, int>(KegState.NoKeg)
            //{
            //    Links =
            //    {
            //        CreateLinkTemplate(LinkRelations.Taps.AddKeg, ReplaceKegSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id )
            //    },
            //    Operations = new StateSpecOperationsSource<Tap, int>()
            //    {
            //        Get = ServiceOperations.Get,
            //        InitialPost = ServiceOperations.Create,
            //        Post = ServiceOperations.Update,
            //        Put = ServiceOperations.Update,
            //        Delete = ServiceOperations.Delete,
            //    }
            //};
            yield return new ResourceStateSpec<Tap, KegState, int>(KegState.New)
            {
                Links =
                {
                    CreateLinkTemplate(LinkRelations.Taps.PourBeer, PourBeerSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id)
                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete,
                }
            };
            yield return new ResourceStateSpec<Tap, KegState, int>(KegState.GoingDown)
            {
                Links =
                {
                      CreateLinkTemplate(LinkRelations.Taps.PourBeer, PourBeerSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id)
                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                    Delete = ServiceOperations.Delete,
                }
            };
            yield return new ResourceStateSpec<Tap, KegState, int>(KegState.AlmostEmpty)
            {
                Links =
                {
                     CreateLinkTemplate(LinkRelations.Taps.ReplaceKeg, ReplaceKegSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id),
                   CreateLinkTemplate(LinkRelations.Taps.PourBeer, PourBeerSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id)

                },
                Operations = new StateSpecOperationsSource<Tap, int>()
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,
                  
                }
            };
            yield return new ResourceStateSpec<Tap, KegState, int>(KegState.SheIsDryMate)
            {
                Links =
                {
                       CreateLinkTemplate(LinkRelations.Taps.ReplaceKeg, ReplaceKegSpec.UriTapAtOffice,c=> c.OfficeId,  c => c.Id)
                },
                Operations =
                {
                    Get = ServiceOperations.Get,
                    InitialPost = ServiceOperations.Create,
                    Post = ServiceOperations.Update,
                    Put = ServiceOperations.Update,

                }
            };
        }
    }
}
  
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Hypermedia;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using MyBeerTap.Model;

namespace MyBeerTap.WebApi
{
    public class TapKegStateProvider: TapKegStateProvider<Tap>
    {
    }

 
    public abstract class TapKegStateProvider<TPlaceResource> : ResourceStateProviderBase<TPlaceResource, KegState>
    where TPlaceResource : IStatefulResource<KegState>, ITapStateful
    {
        public override KegState GetFor(TPlaceResource resource)
        {
            return resource.KegState;
        }
        protected override IDictionary<KegState, IEnumerable<KegState>> GetTransitions()
        {
            return new Dictionary<KegState, IEnumerable<KegState>>
{

             //{ KegState.NoKeg,
             //       new[] { KegState.New }

             //},

            { KegState.New,
                    new[] {
                        KegState.GoingDown,
                        KegState.AlmostEmpty,
                        KegState.SheIsDryMate
                    }

             },
             {KegState.GoingDown,
                    new[]
                    {
                         KegState.New,
                        KegState.AlmostEmpty,
                        KegState.SheIsDryMate
                    }
              },

              {KegState.AlmostEmpty,
                    new[]  {
                        KegState.New,
                        KegState.GoingDown,
                        KegState.SheIsDryMate  }
               },
                 
             
               {KegState.SheIsDryMate,
                    new[]  { KegState.New }},
                };

        }
        public override IEnumerable<KegState> All
        {
            get { return EnumEx.GetValuesFor<KegState>(); }
        }
    }
}
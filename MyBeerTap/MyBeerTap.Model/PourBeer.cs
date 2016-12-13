 

using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace MyBeerTap.Model
{

    /// <summary>
    /// Use this to pour Beer from the Tap
    /// </summary> 
    public class PourBeer : IStatelessResource, IIdentifiable<int>
    {

        /// <summary>
        /// TapId
        /// </summary> 
        public int Id { get; set; }


        /// <summary>
        /// OfficeId
        /// </summary> 
        public int OfficeId { get; set; }


        /// <summary>
        /// Beer Tap
        /// </summary> 
        public Tap Tap { get; set; }

        /// <summary>
        /// Glass of Beer
        /// </summary> 
        public Glass Glass { get; set; }
    }
  
     
}

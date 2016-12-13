using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace MyBeerTap.Model
{

    /// <summary>
    /// Replace Keg 
    /// </summary> 
    public class ReplaceKeg : IStatelessResource, IIdentifiable<int>
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
        /// Keg
        /// </summary> 
        public Keg Keg { get; set; }
    }
}

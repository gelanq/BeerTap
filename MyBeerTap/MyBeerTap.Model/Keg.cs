using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace MyBeerTap.Model
{
    /// <summary>
    /// Sizes of Keg
    /// </summary>

    public enum KegSize
    {
        /// <summary>
        ///Size is SixthBarrel
        /// </summary>
        SixthBarrel,

        /// <summary>
        ///Size is  Quarter Barrel
        /// </summary>
        QuarterBarrel,

        /// <summary>
        ///Size is Half Barrel
        /// </summary>
        HalfBarrel

    }
    /// <summary>
    ///   Keg  
    /// </summary>
    public class Keg : IStatelessResource, IIdentifiable<int>
    {

        /// <summary>
        ///Identity of the the Keg
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///Beer name of the the Keg
        /// </summary>
        public BeerName Beer { get; set; }

        /// <summary>
        ///Capacity of the the Keg
        /// </summary>
        public double Capacity { get; set;  }

        /// <summary>
        ///Size of the Keg
        /// </summary>
        public KegSize Size { get; set; }


        /// <summary>
        ///Remaining beer 
        /// </summary>
        public double Remaining { get; set; }


        /// <summary>
        ///TapId of the the Keg
        /// </summary>
        public int? TapId { get; set; }
    
        internal Tap Tap { get; set; }

    }
}

 
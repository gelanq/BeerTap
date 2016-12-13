 

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyBeerTap.Model
{

    /// <summary>
    /// State of the Keg
    /// </summary> 
    public enum KegState
     {

       ///// <summary>
       ///// No keg
       ///// </summary>
       // NoKeg,

        /// <summary>
        /// Keg is   going down. Not yet available for replacement.
        /// </summary> 
  
        New,
        /// <summary>
        /// Keg is   going down. Not yet available for replacement.
        ///  
        /// </summary> 
        GoingDown,

        /// <summary>
        /// 
        /// Keg is almost empty then a link to “replaceKeg” would be there 
        /// </summary> 
        AlmostEmpty,
        /// <summary>
        /// Keg is dry then a link to “replaceKeg” would be there
        ///  
        /// </summary> 
        SheIsDryMate

    }

    /// <summary>
    /// Beer Tap
    /// </summary> 
    public class Tap :  IStatefulResource<KegState>, IIdentifiable<int>, ITapStateful, IStatelessResource
    {

        /// <summary>
        /// Id
        ///  
        /// </summary> 
        public int Id { get; set; }

        /// <summary>
        /// Label(Beer name) of the Tap
        /// </summary> 
        public string Label { get; set; }

        /// <summary>
        /// Office Id of the Tap
        /// </summary> 
        public int OfficeId { get; set; }


        /// <summary>
        /// State of the Keg(from Keg)
        /// </summary> 
        [NotMapped]
        public KegState KegState {
            get
            {
                Keg k = this.Keg;

                //if (k == null)
                //    return KegState.NoKeg;
                //else
                if (k.Capacity == k.Remaining)
                    return KegState.New;
                else if (k.Remaining < 0.1) //ml
                    return KegState.SheIsDryMate;
                else if (k.Remaining < 500) //ml
                    return KegState.AlmostEmpty; 

                else
                    return KegState.GoingDown;


            }
        }

        /// <summary>
        /// Amount of the Remaining(available) Beer
        /// </summary> 
        [NotMapped]
        public double? RemainingBeer
        {
            get { return (this.Keg == null) ? double.NaN : this.Keg.Remaining; }
        }


        [ForeignKey("OfficeId")]
        private Office Office { get; set; }


        /// <summary>
        /// The current Keg of the Tap
        /// </summary>  
        public virtual Keg Keg { get; set; }
        private List<Glass> Glasses { get; set; }


    }
}
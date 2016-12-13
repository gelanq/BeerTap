using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;

namespace MyBeerTap.Model
{
    /// <summary>
    /// Glass to pour Beer
    /// </summary> 
    public class Glass  
    {


        /// <summary>
        /// Tap Id where to pour the Beer
        /// </summary> 
        public int TapId { get; set; }

        /// <summary>
        /// Identity of the Glass
        /// </summary> 
        
        public int Id { get; set; }

        /// <summary>
        /// Amount of Beer to pour in ml
        /// </summary> 
        public double AmountToPour { get; set; }  
        [ForeignKey("TapId")]
      
        private Tap Tap { get; set; }

 
}
}

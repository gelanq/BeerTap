using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IQ.Platform.Framework.Common;
using IQ.Platform.Framework.WebApi.Model.Hypermedia;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MyBeerTap.Model
{

    /// <summary>
    /// Offices DBSet 
    /// </summary> 
    public class Office : IStatelessResource, IIdentifiable<int>
    {
        /// <summary>
        /// Identity of the Office 
        /// </summary> 

        public int  Id { get; set; }

        /// <summary>
        /// Name of the Office 
        /// </summary> 

        public string Name { get; set; }


        [JsonIgnore]
        private List<Tap> Taps { get; set; }

    }

}
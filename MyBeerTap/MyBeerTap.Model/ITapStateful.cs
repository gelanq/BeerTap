using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBeerTap.Model
{
    /// <summary>
    ///Tap State Interface 
    /// </summary> 
    public interface ITapStateful
    {
        /// <summary>
        ///Current state of the Keg 
        /// </summary> 
        KegState KegState { get;  }
    }
}

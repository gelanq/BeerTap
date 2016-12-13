namespace MyBeerTap.Model
{
    /// <summary>
    /// iQmetrix link relation names
    /// </summary>
    public static class LinkRelations
    {

         /// <summary>
        /// link relation to describe the Identity resource.
        /// </summary>
        public const string Office = "iq:Office";

        /// <summary>
        /// link relation to describe the Tap
        /// </summary>
        public const string Tap = "iq:Tap";

        /// <summary>
        /// link relation to describe the Glass
        /// </summary>
        public const string Glass = "iq:Glass";

        /// <summary>
        /// link relation to describe the Keg
        /// </summary>
        public const string Keg = "iq:Keg";

        /// <summary>
        /// Link for Taps
        /// </summary> 

        public static class Taps
        {
            /// <summary>
            /// Link to describe multiple taps
            /// </summary>
            public const string Plurl = "iq:Taps";
            /// <summary>
             /// action link to replace Keg.
             /// </summary>
            public const string ReplaceKeg = "iq:ReplaceKeg";
            /// <summary>
           /// action link to remove get beer by a glass
            /// </summary>
            public const string PourBeer = "iq:PourBeer";
            /// <summary>
            /// action link to replace Keg..
            /// </summary>
            public const string AddKeg = "iq:AddKeg";

        }
       

    }
}

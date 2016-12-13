using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyBeerTap.Model.Data
{
    /// <summary>
    /// BeerTapDBContext Seeder
    /// </summary> 
    public class BeerTapDBContextSeeder: DropCreateDatabaseIfModelChanges<BeerTapDBContext>
    { 
        
       /// <summary>
       ///Seed
      /// </summary> 
        protected override void Seed(BeerTapDBContext context)
        {
             
            GetOffices().ForEach(o => context.Offices.Add(o));
            GetTaps().ForEach(t => context.Taps.Add(t));
            GetKegs().ForEach(t => context.Kegs.Add(t));
            context.SaveChanges();
     

            foreach (Tap tap in context.Taps.ToList())
            {

              
                context.Kegs.ToList().FindAll(x => x.TapId == tap.Id).ForEach(tk => tap.Keg = tk );
            }
 
            context.SaveChanges();

            base.Seed(context);

        }
            
         


        private static List<Office> GetOffices()
        {
            return new List<Office>
            {
                new Office {
                     Id = 1,
                    Name = "Vancouver",
                    //Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 1, Label =  BeerName.Hoegaarden},
                    //      new Tap(){ Id = 2, Label = BeerName.Bluemoon},
                    //      new Tap(){ Id = 3, Label = BeerName.Molson},
                    //      new Tap(){ Id = 4, Label = BeerName.Heineken},
                    //}
                
                },
                new Office {
                     Id = 2,
                    Name = "Regina",
                    //  Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 5, Label =  BeerName.Hoegaarden},
                    //      new Tap(){ Id = 6, Label = BeerName.Heineken},
                    //      new Tap(){ Id = 7, Label = BeerName.Molson},
                       

                    //}
                },
                new Office {
                        Id = 3,
                        Name = "Winnipeg",
                    //      Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 8, Label =  BeerName.Hoegaarden},
                    //      new Tap(){ Id = 9, Label = BeerName.Heineken},
                    //      new Tap(){ Id = 10, Label = BeerName.Molson}
                    //}

                },
                new Office {
                        Id = 4,
                        Name = "Davidson(NC)",
                    //        Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 11, Label =  BeerName.Snozzberry},
                    //      new Tap(){ Id = 12, Label = BeerName.Jade},
                    //      new Tap(){ Id = 13, Label = BeerName.MoeBeer}
                    //}
                },
                new Office {
                     Id = 5,
                     Name = "Manila Philippines",
                    //  Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 14, Label =  BeerName.SMBPale},
                    //      new Tap(){ Id = 15, Label = BeerName.Hoegaarden},
                    //      new Tap(){ Id = 16, Label = BeerName.SMBLight}
                    //}
                },
                new Office {
                     Id = 6,
                     Name = "Sydney Australia",
                    //   Taps = new List<Tap>()
                    //{
                    //      new Tap(){ Id = 17, Label =  BeerName.HopHog},
                           
                    //}
                } 

            };
        }


        private static List<Tap> GetTaps()
        {
            return new List<Tap>
            {

                new Tap {Id = 1, Label = BeerName.Hoegaarden.ToString(), OfficeId = 1},
                new Tap {Id = 2, Label = BeerName.Bluemoon.ToString(), OfficeId = 1},
                new Tap {Id = 3, Label = BeerName.Molson.ToString(), OfficeId =  1},
                new Tap {Id = 4, Label = BeerName.Heineken.ToString(), OfficeId = 1},


                new Tap(){ Id = 5, Label =  BeerName.Hoegaarden.ToString(),  OfficeId = 2},
                new Tap(){ Id = 6, Label = BeerName.Heineken.ToString(),  OfficeId = 2},
                new Tap(){ Id = 7, Label = BeerName.Molson.ToString(),  OfficeId = 2},


                 new Tap(){ Id = 8, Label =  BeerName.Hoegaarden.ToString(),  OfficeId = 3},
                 new Tap(){ Id = 9, Label = BeerName.Heineken.ToString(),  OfficeId = 3},
                 new Tap(){ Id = 10, Label = BeerName.Molson.ToString(),  OfficeId = 3},

                 new Tap(){ Id = 11, Label =  BeerName.Snozzberry.ToString(),  OfficeId = 4},
                 new Tap(){ Id = 12, Label = BeerName.Jade.ToString(),  OfficeId = 4},
                 new Tap(){ Id = 13, Label = BeerName.MoeBeer.ToString(),  OfficeId = 4},

                 new Tap(){ Id = 14, Label = BeerName.SmbPale.ToString(),  OfficeId = 5},
                 new Tap(){ Id = 15, Label = BeerName.Hoegaarden.ToString(),  OfficeId = 5},
                 new Tap(){ Id = 16, Label = BeerName.SmbPale.ToString(),  OfficeId = 5},

                 new Tap(){ Id = 17, Label =  BeerName.HopHog.ToString(),  OfficeId = 6},

            };
        }

        private static List<Keg> GetKegs()
        {
            return new List<Keg>
            {

                new Keg {Id = 1, Beer = BeerName.Hoegaarden, Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 1  }, 
                new Keg {Id = 2, Beer = BeerName.Bluemoon ,   Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 2},
                new Keg {Id = 3, Beer = BeerName.Molson, Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 3},
                new Keg {Id = 4, Beer = BeerName.Heineken,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 4},


                new Keg {Id = 5, Beer = BeerName.Hoegaarden, Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 5 },
                new Keg {Id = 6, Beer = BeerName.Heineken ,   Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 6},
                new Keg {Id = 7, Beer = BeerName.Molson, Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 7},


                new Keg {Id = 8, Beer = BeerName.Hoegaarden,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 8},
                new Keg {Id = 9, Beer = BeerName.Heineken,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 9},
                new Keg {Id = 10, Beer = BeerName.Molson,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 10},

                new Keg {Id = 11, Beer = BeerName.Snozzberry,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 11},
                new Keg {Id = 12, Beer = BeerName.Jade,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 12},
                new Keg {Id = 13, Beer = BeerName.MoeBeer,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 13},

                new Keg {Id = 14, Beer = BeerName.SmbPale,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 14},
                new Keg {Id = 15, Beer = BeerName.Hoegaarden,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 15},
                new Keg {Id = 16, Beer = BeerName.SmbLight,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 16},

                new Keg {Id = 17, Beer = BeerName.HopHog,  Size = KegSize.SixthBarrel, Capacity = 19532.72, Remaining = 19532.72, TapId = 17}



            };
        }
    }
}

 
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
 

namespace MyBeerTap.Model.Data
{

    /// <summary>
    /// The Repository
    /// </summary>
    public class BeeerTapRepository
    {
        private BeerTapDBContext _dbContext;

        #region "Office"

        /// <summary>
        ///Get all Offices
        /// </summary>
        public IEnumerable<Office> GetOffices()
            {
                _dbContext = new BeerTapDBContext();
             

            //var subcate = (from a in _dbContext.Offices
            //               join b in _dbContext.Taps on a.Id equals b.OfficeId
            //               select new  
            //               {
            //                   Id = a.Id,
            //                   Name = a.Name 
            //               }).ToList().Select( x => new Office()
            //    {
            //        Id = x.Id,
            //        Name = x.Name
            //    }
            //    );
            return _dbContext.Offices ;


        }
        /// <summary>
        ///Get Office Details by Id
        /// </summary>
        public Office GetOfficeById(int id)
            {
                _dbContext = new BeerTapDBContext();
                var office = _dbContext.Offices.Where(o => o.Id == id).FirstOrDefault();
                return office;
            }

        /// <summary>
        ///Add new Office
        /// </summary>
        public Office AddOffice(Office office)
            {
                _dbContext = new BeerTapDBContext();
                _dbContext.Offices.Add(office);
                _dbContext.SaveChanges();
                return office; //_dbContext.Offices.Where(o => o.Id == office.Id).FirstOrDefault(); 
            }

        /// <summary>
        ///Update Office
        /// </summary>
        public Office  UpdateOffice(Office office)
            {

                _dbContext = new BeerTapDBContext();

                _dbContext.Entry(office).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();
                return office;
            }

        /// <summary>
        ///Remove an Office
        /// </summary>
        public void RemoveOffice(Office office)
            {

                _dbContext = new BeerTapDBContext();

                _dbContext.Entry(office).State = System.Data.Entity.EntityState.Deleted;
                _dbContext.SaveChanges();
            
            }
        #endregion

        #region Tap
        /// <summary>
        ///Get Beer Taps int the office
        /// </summary>
        public IEnumerable<Tap> GetTaps(int officeId)
        {
            _dbContext = new BeerTapDBContext();
            return _dbContext.Taps.Where(i => i.OfficeId == officeId).Include("Keg");
        }
        /// <summary>
        ///Get Beer Tap Details 
        /// </summary>
        public Tap GetTapById(int id)
        {
            _dbContext = new BeerTapDBContext();
             return _dbContext.Taps.Where(o => o.Id == id).Include("Keg").FirstOrDefault();
 

        }
        /// <summary>
        ///Update Beer Tap 
        /// </summary>
        public Tap UpdateTap(Tap tap)
        {

            _dbContext = new BeerTapDBContext();

            _dbContext.Entry(tap).State = System.Data.Entity.EntityState.Modified;
            _dbContext.Kegs.Add(tap.Keg);
            _dbContext.SaveChanges();
            return _dbContext.Taps.Where(o => o.Id == tap.Id).Include("Keg").FirstOrDefault(); 
        }

        /// <summary>
        ///Add new Tap
        /// </summary>
        public Tap AddTap(Tap tap)
        {
            
       
            _dbContext = new BeerTapDBContext();
            _dbContext.Taps.Add(tap);
            _dbContext.SaveChanges();
            return _dbContext.Taps.Where(o => o.Id == tap.Id).FirstOrDefault();
        }

        /// <summary>
        ///Remove a tap
        /// </summary>
        public void RemoveTap(Tap tap)
        {

            _dbContext = new BeerTapDBContext();

            _dbContext.Entry(tap).State = System.Data.Entity.EntityState.Deleted;
            _dbContext.SaveChanges();

        }
        #endregion

        #region Glass

        /// <summary>
        ///Add new Glass to pour beer from the tap
        /// </summary>
        public Glass AddGlass(Glass glass)
        {
            _dbContext = new BeerTapDBContext();
            _dbContext.Glasses.Add(glass);
            _dbContext.SaveChanges();
            return glass;
        }

        /// <summary>
        ///Get all glasses transactions
        /// </summary>
        public IEnumerable<Glass> GetGlasses(int tapId)
        {
            _dbContext = new BeerTapDBContext();
            return _dbContext.Glasses.Where(o => o.TapId == tapId);
        }
        #endregion


        #region Keg

        /// <summary>
        ///Update the Keg by Glass(pour beer)
        /// </summary>
        public Keg UpdateKegByGlass(Glass g)
            {
            _dbContext = new BeerTapDBContext();
         
            Keg keg = _dbContext.Kegs.Where(k => k.TapId == g.TapId).FirstOrDefault<Keg>();
            keg.Remaining -= g.AmountToPour;
            _dbContext.SaveChanges();

            return null;
            }

        /// <summary>
        ///Update Keg details
        /// </summary>
        public Keg UpdateKeg(Keg keg)
        {
            _dbContext = new BeerTapDBContext();

            _dbContext.SaveChanges();
            return _dbContext.Kegs.Where(o => o.Id == keg.Id).FirstOrDefault();

 
        }

        /// <summary>
        ///Get all kegs 
        /// </summary>
        public IEnumerable<Keg> GetKegs()
        {
            _dbContext = new BeerTapDBContext();
            return _dbContext.Kegs.Include("Tap");
        }


        /// <summary>
        ///Get the current Keg of the Tap
        /// </summary>
        public Keg GetKegByTapId(int tapId)
        {
            _dbContext = new BeerTapDBContext();
            return _dbContext.Kegs.Where(k => k.TapId == tapId).FirstOrDefault();
        }


        /// <summary>
        ///Replace Keg
        /// </summary>
        public Keg ReplaceKeg(Keg keg )
        {
            _dbContext = new BeerTapDBContext();

            Keg oldKeg =  GetKegByTapId(keg.TapId ?? 0);
            if (oldKeg != null)
            {
                //oldKeg.Tap = null;
                oldKeg.TapId = null;


            }


            Tap tap = _dbContext.Taps.Where(o => o.Id == keg.TapId).Include("Keg").FirstOrDefault();
            tap.Keg = keg;
            _dbContext.Kegs.Add(keg);
       
            _dbContext.SaveChanges();


            return keg;
        }

      
        #endregion



    }
}

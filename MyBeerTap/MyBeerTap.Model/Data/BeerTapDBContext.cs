using System.ComponentModel.DataAnnotations.Schema;

namespace MyBeerTap.Model
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// BeerTapDBContext
    /// </summary> 
    public class BeerTapDBContext : DbContext
    {

        /// <summary>
        /// Override the name of the DB
        /// </summary> 
        public BeerTapDBContext()
            : base("name=BeerTapDBContext")
        {
           
           
        }

        /// <summary>
        /// Offices DBSet 
        /// </summary> 
        public DbSet<Office> Offices { get; set; }

        /// <summary>
        /// Taps DBSet 
        /// </summary> 
        public DbSet<Tap> Taps { get; set; }

        /// <summary>
        /// Kegs DBSet 
        /// </summary> 
        public DbSet<Keg> Kegs { get; set; }

        /// <summary>
        /// Glasses DBSet 
        /// </summary> 
        public DbSet<Glass> Glasses { get; set; }


        /// <summary>
        /// Fluent API 
        /// </summary> 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

         
            modelBuilder.Entity<Keg>()
                    .HasOptional(s => s.Tap)
                    .WithMany()
                    .HasForeignKey(d => d.TapId);

        }


    }


    }





 
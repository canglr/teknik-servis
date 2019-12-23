using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using teknikservis.Models;


namespace TeknikServis.Data.DataContext
{
    public class TeknikContext : DbContext
    {

         public TeknikContext()
            : base("name=TeknikContext")
        {
        }


        public DbSet<Kullanici> Kullanici { get; set; }

        public DbSet<Grup> Grup { get; set; }

        public DbSet<Oturumlar> Oturumlar { get; set; }

        public DbSet<Servisler> Servisler { get; set; }

        public DbSet<Ayarlar> Ayarlar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grup>()
                .HasMany(e => e.Kullanici)
                .WithOptional(e => e.Grup)
                .HasForeignKey(e => e.Grup_ID);

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Oturumlar)
                .WithOptional(e => e.Kullanici)
                .HasForeignKey(e => e.Kullanici_ID);
        }


    }
}

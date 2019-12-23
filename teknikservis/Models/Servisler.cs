using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace teknikservis.Models
{
    [Table("Servisler")]
    public partial class Servisler
    {
        public int ID { get; set; }        

        public string Musteri { get; set; }

        public string Tel { get; set; }
       
        public int Durum { get; set; }
        
        public int Cihaz { get; set; }

        public string Model { get; set; }

        public string Adres { get; set; }

        public string Aciklama { get; set; }

        public DateTime Olusturulma_Tarihi { get; set; }
        
        public int Sil { get; set; }

        public decimal Fiyat { get; set; }

        public decimal Tahsilat { get; set; }

        public string Yapilan_İslem { get; set; }

        public DateTime Guncellenme_Tarihi { get; set; }

        public int Son_İslem_Yapan { get; set; }

        public int? Kullanici_ID { get; set; }

        public virtual Kullanici Kullanici { get; set; }

    }
}
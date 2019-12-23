using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teknikservis.ViewModels
{
    public class ServislerListeModel
    {

        public int ID { get; set; }

        public int? Kullanıcı_ID { get; set; }

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


    }
}
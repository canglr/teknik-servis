using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teknikservis.ViewModels
{
    public class KullaniciListeModel
    {
        public int ID { get; set; }
       
        public string Ad { get; set; }
       
        public string Soyad { get; set; }
       
        public string Mail { get; set; }
        
        public string Kullanici_Adi { get; set; }
        
        public string Sifre { get; set; }

        public int Durum { get; set; }

        public DateTime Kayit_Tarihi { get; set; }

        public DateTime Guncellenme { get; set; }

        public int? Grup_ID { get; set; }
        
    }
}
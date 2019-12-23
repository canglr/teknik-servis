using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teknikservis.ViewModels
{
    public class OturumListeModel
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public DateTime Giris_Tarihi { get; set; }
        public int Durum { get; set; }
        public string Kullanici_Adi { get; set; }
        public string Kullanici_Ad { get; set; }
        public string Kullanici_Soyad { get; set; }
    }
}
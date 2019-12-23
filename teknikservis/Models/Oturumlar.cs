namespace teknikservis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

      
    [Table("Oturumlar")]
    public partial class Oturumlar
    {
        public int ID { get; set; }

        public Guid Kimlik { get; set; }

        [StringLength(15)]
        public string Ip { get; set; }

        public int Durum { get; set; }

        public DateTime Giris_Tarihi { get; set; }

        public int? Kullanici_ID { get; set; }

        public virtual Kullanici Kullanici { get; set; }
    }
}

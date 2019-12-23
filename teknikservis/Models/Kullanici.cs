namespace teknikservis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kullanici")]
    public partial class Kullanici
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Kullanici()
        {
            Oturumlar = new HashSet<Oturumlar>();
        }

        public int ID { get; set; }

        [StringLength(50)]
        public string Ad { get; set; }

        [StringLength(50)]
        public string Soyad { get; set; }

        [Required]
        public string Mail { get; set; }

        [Required]
        public string Kullanici_Adi { get; set; }

        [Required]
        public string Sifre { get; set; }

        public int Durum { get; set; }

        public DateTime Kayit_Tarihi { get; set; }

        public DateTime Guncellenme { get; set; }

        public int? Grup_ID { get; set; }

        public virtual Grup Grup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Oturumlar> Oturumlar { get; set; }
    }
}

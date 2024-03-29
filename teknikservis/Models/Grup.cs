namespace teknikservis.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Grup")]
    public partial class Grup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grup()
        {
            Kullanici = new HashSet<Kullanici>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Grup_Ad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Kullanici> Kullanici { get; set; }
    }
}

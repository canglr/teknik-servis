using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace teknikservis.Models
{
    [Table("Ayarlar")]
    public class Ayarlar
    {

        public int ID { get; set; }

        public string Site_Adi { get; set; }

        public string Site_Adresi { get; set; }

    }
}
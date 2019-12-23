using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace teknikservis.ViewModels
{
    public class KullaniciModel
    {

        public int? sayfa { get; set; }
        public IPagedList<KullaniciListeModel> Kullanicilar { get; set; }

    }
}
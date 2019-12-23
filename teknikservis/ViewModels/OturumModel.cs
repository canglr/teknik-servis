using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace teknikservis.ViewModels
{
    public class OturumModel
    {
        public int? sayfa { get; set; }
        public IPagedList<OturumListeModel> Oturumlar { get; set; }
    }
}
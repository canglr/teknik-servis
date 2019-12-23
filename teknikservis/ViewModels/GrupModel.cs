using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace teknikservis.ViewModels
{
    public class GrupModel
    {
        public int? sayfa { get; set; }
        public IPagedList<GrupListeModel> Gruplar { get; set; }
    }
}
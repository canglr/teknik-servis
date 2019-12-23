using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace teknikservis.ViewModels
{
    public class ServislerModel
    {
        public int? sayfa { get; set; }        
        public IPagedList<ServislerListeModel> Servisler { get; set; }
    }
}
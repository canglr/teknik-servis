using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using teknikservis.ViewModels;
using TeknikServis.Data.DataContext;

namespace teknikservis.Controllers
{
    public class OturumlarController : Controller
    {
        // GET: Oturumlar
        TeknikContext db = new TeknikContext();
        public ActionResult Index(OturumModel model)
        {
            if (Session["kimlik"] != null)
            {
                int sayfano = model.sayfa ?? 1;
                model.Oturumlar = (from oturum in db.Oturumlar
                                   join kullanici in db.Kullanici on oturum.Kullanici_ID equals kullanici.ID
                                   orderby (oturum.ID) descending

                                   select new OturumListeModel
                                   {
                                       Durum = oturum.Durum,
                                       ID = oturum.ID,
                                       IP = oturum.Ip,
                                       Giris_Tarihi = oturum.Giris_Tarihi,
                                       Kullanici_Adi = kullanici.Kullanici_Adi,
                                       Kullanici_Ad = kullanici.Ad,
                                       Kullanici_Soyad = kullanici.Soyad


                                   }).ToPagedList(sayfano, 16);

                if (Request.IsAjaxRequest())
                { return PartialView("_Oturumlar", model); }
                else
                { return View(model); }
            }
            else { return View(); }

            
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Data.DataContext;
using teknikservis.Models;
using teknikservis.ViewModels;

namespace teknikservis.Controllers{
     
    public class AnaSayfaController : Controller
    {
        // GET: AnaSayfa
        TeknikContext db = new TeknikContext();
        public ActionResult Index()
        {

            var servistoplam = db.Servisler.Count();
            var servisbekleyen = db.Servisler.Count(b => b.Durum == 1);
            var servisyapildi = db.Servisler.Count(y => y.Durum == 2);
            var servisiade = db.Servisler.Count(i => i.Durum == 3);
            ViewData["servistoplam"] = servistoplam;
            ViewData["servisyapildi"] = servisyapildi;
            ViewData["servisbekleyen"] = servisbekleyen;
            ViewData["servisiade"] = servisiade;
            var toplamfiyat = db.Servisler.Sum(f => f.Fiyat);
            var toplamtahsilat = db.Servisler.Sum(t => t.Tahsilat);
            var toplamkazanc = toplamfiyat - toplamtahsilat;
            ViewData["toplamfiyat"] = toplamfiyat;
            ViewData["toplamtahsilat"] = toplamtahsilat;
            ViewData["toplamkazanc"] = toplamkazanc;
            return View();
        }
    }
}
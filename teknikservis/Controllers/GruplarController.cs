using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TeknikServis.Data.DataContext;
using teknikservis.Models;
using teknikservis.ViewModels;
using PagedList;

namespace teknikservis.Controllers
{
    public class GruplarController : Controller
    {
        // GET: Gruplar
       TeknikContext db =  new TeknikContext();

        public class GrupBilgi
        {
            
            public string mesaj { get; set; }          

        }


        public ActionResult Index(GrupModel model)
        {
            if (Session["kimlik"] != null)
            {
                int sayfano = model.sayfa ?? 1;
                model.Gruplar = (from Grup in db.Grup
                                 orderby Grup.ID descending

                                 select new GrupListeModel
                                 {
                                     ID = Grup.ID,
                                     Grup_Ad = Grup.Grup_Ad,

                                 }).ToPagedList(sayfano, 15);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Gruplar", model);
                }
                else { return View(model); }
            }
            else
            {
                return View();
            }
            }


        public JsonResult YeniGrup(String ad)
        {
            if (Session["kimlik"] != null)
            {
                GrupBilgi bilgi = new GrupBilgi();
                Grup grup = new Grup();
                grup.Grup_Ad = ad;
                db.Grup.Add(grup);
                db.SaveChanges();
                bilgi.mesaj = "Grup Başarıyla Oluşturuldu !";
                return Json(bilgi);
            }
            else
            {
                return null;
            }
        }       


        public ActionResult Guncelle(String id)
        {
            if (Session["kimlik"] != null)
            {

                int ara = int.Parse(Guvenlik.SİFRELECOZ(id));
                var veri = db.Grup.FirstOrDefault(k => k.ID == ara);
                if (veri == null)
                {
                    return HttpNotFound();
                }
                else
                {


                    return PartialView("Guncelle", veri);
                }
            }
            else
            {
                return null;
            }

        }


        public JsonResult GuncellePost(String id, String ad)
        {
            if (Session["kimlik"] != null)
            {
                GrupBilgi bilgi = new GrupBilgi();
                int sid = Convert.ToInt32(Guvenlik.SİFRELECOZ(id));
                var veri = db.Grup.Where(s => s.ID == sid).FirstOrDefault();
                veri.Grup_Ad = ad;
                db.SaveChanges();
                bilgi.mesaj = "başarıyla güncellendi !";
                return Json(bilgi);
            }
            else
            {
                return null;
            }
        }


        public ActionResult Sil(String id)
        {
            if (Session["kimlik"] != null)
            {
                int ara = int.Parse(teknikservis.Controllers.Guvenlik.SİFRELECOZ(id));
                var veri = db.Grup.FirstOrDefault(k => k.ID == ara);
                db.Grup.Remove(veri);
                db.SaveChanges();
                Response.Redirect("/Gruplar");
                return null;
            }
            else
            {
                return null;
            }
        }


        }
    }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknikservis.ViewModels;
using TeknikServis.Data.DataContext;
using PagedList;
using PagedList.Mvc;
using teknikservis.Models;

namespace teknikservis.Controllers
{
    public class ServislerController : Controller
    {
        // GET: Servisler
        public class Bilgi
        {
            public string mesaj { get; set; }
            public int servisno { get; set; }

        }


         public class Gonder
        {
            public int servisno { get; set; }
            public string musteri { get; set; }
            public string tel { get; set; }
            public string durum { get; set; }
            public string cihaz { get; set; }
            public string model { get; set; }
            public string adres { get; set; }
            public string aciklama { get; set; }
            public string yapilanislem { get; set; }
            public string Olusturulma { get; set; }
            public decimal fiyat { get; set; }
            public decimal tahsilat { get; set; }
            public string Guncellenme { get; set; }
            public string kayiteden { get; set; }
            public string songuncelleyen { get; set; }
            public string tarih { get; set; }

        }


        TeknikContext db =  new TeknikContext();
        public ActionResult Index(ServislerModel model, FormCollection form)
        {
            if (Session["kimlik"] != null)
            {
                int sayfano = model.sayfa ?? 1;

                if (Request.HttpMethod == "POST")
                {

                    if (form["arasec"] == "1")
                    {
                        int ara = int.Parse(form["ara"]);

                        model.Servisler = (from servis in db.Servisler
                                           where servis.Sil.ToString() == "0" && servis.ID == ara
                                           orderby servis.ID descending

                                           select new ServislerListeModel
                                           {
                                               ID = servis.ID,
                                               Musteri = servis.Musteri,
                                               Tel = servis.Tel,
                                               Durum = servis.Durum,
                                               Model = servis.Model,
                                               Cihaz = servis.Cihaz,
                                               Adres = servis.Adres,
                                               Aciklama = servis.Aciklama,
                                               Olusturulma_Tarihi = servis.Olusturulma_Tarihi,
                                               Fiyat = servis.Fiyat,
                                               Tahsilat = servis.Tahsilat,
                                               Yapilan_İslem = servis.Yapilan_İslem,
                                               Guncellenme_Tarihi = servis.Guncellenme_Tarihi,
                                               Son_İslem_Yapan = servis.Son_İslem_Yapan,
                                               Kullanıcı_ID = servis.Kullanici_ID


                                           }).ToPagedList(sayfano, 100);

                    }
                    else if (form["arasec"] == "2")
                    {

                        String ara = form["ara"];

                        model.Servisler = (from servis in db.Servisler
                                           where servis.Sil.ToString() == "0" && servis.Musteri.Contains(ara)
                                           orderby servis.ID descending

                                           select new ServislerListeModel
                                           {
                                               ID = servis.ID,
                                               Musteri = servis.Musteri,
                                               Tel = servis.Tel,
                                               Durum = servis.Durum,
                                               Model = servis.Model,
                                               Cihaz = servis.Cihaz,
                                               Adres = servis.Adres,
                                               Aciklama = servis.Aciklama,
                                               Olusturulma_Tarihi = servis.Olusturulma_Tarihi,
                                               Fiyat = servis.Fiyat,
                                               Tahsilat = servis.Tahsilat,
                                               Yapilan_İslem = servis.Yapilan_İslem,
                                               Guncellenme_Tarihi = servis.Guncellenme_Tarihi,
                                               Son_İslem_Yapan = servis.Son_İslem_Yapan,
                                               Kullanıcı_ID = servis.Kullanici_ID


                                           }).ToPagedList(sayfano, 100);



                    }

                }
                else
                {


                    model.Servisler = (from servis in db.Servisler
                                       where servis.Sil.ToString() == "0"
                                       orderby servis.ID descending

                                       select new ServislerListeModel
                                       {
                                           ID = servis.ID,
                                           Musteri = servis.Musteri,
                                           Tel = servis.Tel,
                                           Durum = servis.Durum,
                                           Model = servis.Model,
                                           Cihaz = servis.Cihaz,
                                           Adres = servis.Adres,
                                           Aciklama = servis.Aciklama,
                                           Olusturulma_Tarihi = servis.Olusturulma_Tarihi,
                                           Fiyat = servis.Fiyat,
                                           Tahsilat = servis.Tahsilat,
                                           Yapilan_İslem = servis.Yapilan_İslem,
                                           Guncellenme_Tarihi = servis.Guncellenme_Tarihi,
                                           Son_İslem_Yapan = servis.Son_İslem_Yapan,
                                           Kullanıcı_ID = servis.Kullanici_ID


                                       }).ToPagedList(sayfano, 15);
                }
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Servisler", model);
                }
                else { return View(model); }
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult YeniServis(string musteri,string telefon,string adres,string model,int cihaz,string aciklama)
        {
            if(Session["kimlik"] != null)
            { 

            Bilgi bilgi = new Bilgi();
            Servisler servis = new Servisler();
            servis.Musteri = musteri;
            servis.Tel = telefon;
            servis.Adres = adres;
            servis.Model = model;
            servis.Cihaz = cihaz;
            servis.Durum = 1;
            servis.Aciklama = aciklama;
            servis.Olusturulma_Tarihi = DateTime.Now;
            servis.Guncellenme_Tarihi = DateTime.Now;
            servis.Fiyat = 0;
            servis.Tahsilat = 0;
            servis.Sil = 0;
            servis.Son_İslem_Yapan = Convert.ToInt32(Session["id"]);
            servis.Kullanici_ID = Convert.ToInt32(Session["id"]);            
            db.Servisler.Add(servis);            
            db.SaveChanges();
            //bilgi.mesaj = "Müşteri No:"+servis.ID.ToString();
            bilgi.mesaj = "<div class=\"alert alert-success\" role=\"alert\">Servis No:"+servis.ID.ToString()+"</div>";  
            return Json(bilgi);
            }
            else
            {
                return null;
            }
        }

        public ActionResult Goruntule(String id,ServislerModel model)
        {
            if (Session["kimlik"] != null)
            {
                int ara = int.Parse(Guvenlik.SİFRELECOZ(id));
                var veri = db.Servisler.Where(s => s.Sil == 0).FirstOrDefault(f => f.ID == ara);
                if (veri == null)
                {
                    return HttpNotFound();
                }
                else
                {


                    return PartialView("Goruntule", veri);
                }
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
                var veri = db.Servisler.Where(s => s.Sil == 0).FirstOrDefault(f => f.ID == ara);

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

        public JsonResult GuncellePost(String id,String musteri,String telefon,String adres,String model,int cihaz,int durum,String aciklama,decimal fiyat,decimal tahsilat,String Yapilan_İslem)
        {
            if (Session["kimlik"] != null)
            {
                Bilgi bilgi = new Bilgi();
                int sid = Convert.ToInt32(Guvenlik.SİFRELECOZ(id));
                var veri = db.Servisler.Where(s => s.ID == sid).FirstOrDefault();
                veri.Musteri = musteri;
                veri.Tel = telefon;
                veri.Adres = adres;
                veri.Model = model;
                veri.Cihaz = cihaz;
                veri.Durum = durum;
                veri.Aciklama = aciklama;
                veri.Fiyat = fiyat;
                veri.Tahsilat = tahsilat;
                veri.Yapilan_İslem = Yapilan_İslem;
                veri.Guncellenme_Tarihi = DateTime.Now;
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
                int ara = int.Parse(Guvenlik.SİFRELECOZ(id));
                var veri = db.Servisler.FirstOrDefault(f => f.ID == ara);
                veri.Sil = 1;
                db.SaveChanges();
                Response.Redirect("/Servisler");
                return null;
            }
            else
            {
                return null;
            }
        }

        
    }
}
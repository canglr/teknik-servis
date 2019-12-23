using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknikservis.ViewModels;
using teknikservis.Models;
using TeknikServis.Data.DataContext;
using PagedList;
using PagedList.Mvc;

namespace teknikservis.Controllers
{
    public class KullanicilarController : Controller
    {
        // GET: Kullanicilar
        TeknikContext db =  new TeknikContext();

        public class KullaniciBilgi
        {
            
            public string mesaj { get; set; }          

        }


        public ActionResult Index(KullaniciModel model)
        {
            if (Session["kimlik"] != null)
            {
                var grup = db.Grup.ToArray();
                ViewData["gruplar"] = grup;
                int sayfano = model.sayfa ?? 1;
                model.Kullanicilar = (from kullanici in db.Kullanici
                                      orderby kullanici.ID descending

                                      select new KullaniciListeModel
                                      {
                                          ID = kullanici.ID,
                                          Ad = kullanici.Ad,
                                          Soyad = kullanici.Soyad,
                                          Mail = kullanici.Mail,
                                          Kullanici_Adi = kullanici.Kullanici_Adi,
                                          Durum = kullanici.Durum,
                                          Kayit_Tarihi = kullanici.Kayit_Tarihi,
                                          Guncellenme = kullanici.Guncellenme,
                                          Grup_ID = kullanici.Grup_ID


                                      }).ToPagedList(sayfano, 15);

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Kullanicilar", model);
                }
                else { return View(model); }
            }
            else
            {
                return View();
            }
            }


        public JsonResult YeniKullanici(String ad,String soyad,String mail,String kullaniciadi,String sifre,String grup)
        {

            KullaniciBilgi bilgi = new KullaniciBilgi();
            Kullanici kullanici = new Kullanici();
            kullanici.Ad = ad;
            kullanici.Soyad = soyad;
            kullanici.Mail = mail;
            kullanici.Kullanici_Adi = kullaniciadi;
            kullanici.Sifre = teknikservis.Controllers.Guvenlik.MD5S(sifre);
            kullanici.Grup_ID = int.Parse(teknikservis.Controllers.Guvenlik.SİFRELECOZ(grup));
            kullanici.Kayit_Tarihi = DateTime.Now;
            kullanici.Guncellenme = DateTime.Now;
            kullanici.Durum = 1;
            db.Kullanici.Add(kullanici);
            db.SaveChanges();
            bilgi.mesaj = "Kullanıcı Başarıyla Oluşturuldu !";
            return Json(bilgi);
        }



        public ActionResult Goruntule(String id, KullaniciModel model)
        {
            if (Session["kimlik"] != null)
            {
                int ara = int.Parse(Guvenlik.SİFRELECOZ(id));
                var veri = db.Kullanici.FirstOrDefault(f => f.ID == ara);
                var grup = db.Grup.FirstOrDefault(g => g.ID == veri.Grup_ID);
                ViewData["grup"] = grup.Grup_Ad;
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
                var veri = db.Kullanici.FirstOrDefault(k => k.ID == ara);
                var grup = db.Grup.ToArray();
                ViewData["gruplar"] = grup;
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


        public JsonResult GuncellePost(String id, String ad, String soyad, String mail, String kullaniciadi, int durum, String grup, String sifre)
        {
            if (Session["kimlik"] != null)
            {
                KullaniciBilgi bilgi = new KullaniciBilgi();
                int sid = Convert.ToInt32(Guvenlik.SİFRELECOZ(id));
                var veri = db.Kullanici.Where(s => s.ID == sid).FirstOrDefault();
                veri.Ad = ad;
                veri.Soyad = soyad;
                veri.Mail = mail;
                veri.Kullanici_Adi = kullaniciadi;
                veri.Durum = durum;
                veri.Grup_ID = int.Parse(teknikservis.Controllers.Guvenlik.SİFRELECOZ(grup));
                if (sifre != null)
                {
                    veri.Sifre = teknikservis.Controllers.Guvenlik.MD5S(sifre);
                }
                veri.Guncellenme = DateTime.Now;
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
                var veri = db.Kullanici.FirstOrDefault(k => k.ID == ara);
                veri.Durum = 0;
                db.SaveChanges();
                Response.Redirect("/Kullanicilar");
                return null;
            }
            else
            {
                return null;
            }
        }


        }
    }


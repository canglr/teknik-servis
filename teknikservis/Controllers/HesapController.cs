using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknikservis.Models;
using teknikservis.ViewModels;
using TeknikServis.Data.DataContext;
using PagedList;
using PagedList.Mvc;
using System.Dynamic;

namespace teknikservis.Controllers
{
    public class HesapController : Controller
    {
        TeknikContext db = new TeknikContext();

        public class Bilgi
        {
            public string mesaj { get; set; }

        }
        

        public ActionResult Index(OturumModel model)
        {

            if (Session["kimlik"] != null)
            {
                int id = Int32.Parse(Session["id"].ToString());
                int sayfano = model.sayfa ?? 1;
                model.Oturumlar = (from oturum in db.Oturumlar
                                   join kullanici in db.Kullanici on oturum.Kullanici_ID equals kullanici.ID
                                   where oturum.Kullanici_ID == id
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

                var veri = db.Kullanici.Find(id);
                ViewData["Kullanici_Adi"] = veri.Kullanici_Adi;
                ViewData["Ad"] = veri.Ad;
                ViewData["Soyad"] = veri.Soyad;
                ViewData["Mail"] = veri.Mail;
                ViewData["Kayit_Tarihi"] = veri.Kayit_Tarihi;
                ViewData["Guncellenme"] = veri.Guncellenme;
                dynamic mymodel = new ExpandoObject();


                if (Request.IsAjaxRequest())
                { return PartialView("_Oturumlar", model); }
                else
                { return View(model); }

            }
            else
            {
                return View();
            }

        }

        public JsonResult HesapGuncelle(String ad,String soyad,String mail)
        {
            if (Session["kimlik"] != null)
            {
                Bilgi bilgi = new Bilgi();
                int id = Convert.ToInt32(Session["id"]);

                if (ad != "" && soyad != "" && mail != "")
                {
                    var veri = db.Kullanici.Where(k => k.ID == id).FirstOrDefault();

                    veri.Ad = ad;
                    veri.Soyad = soyad;
                    veri.Mail = mail;
                    veri.Guncellenme = DateTime.Now;

                    Session["Ad"] = ad;
                    Session["Soyad"] = soyad;
                    Session["Mail"] = mail;

                    db.SaveChanges();
                    bilgi.mesaj = "Bilgileriniz Güncellendi !";
                }
                else
                {
                    bilgi.mesaj = "Ad Soyad ve Mail alanlarını boş bırakmayınız !";
                }

                return Json(bilgi);
            }
            else
            {
                return null;
            }
        }



        public JsonResult SifreGuncelle(String mevcutsifre, String yenisifre, String yenisifretekrar)
        {
            if (Session["kimlik"] != null)
            {
                Bilgi bilgi = new Bilgi();
                int id = Convert.ToInt32(Session["id"]);

                if (mevcutsifre != "" && yenisifre != "" && yenisifretekrar != "")
                {
                    mevcutsifre = teknikservis.Controllers.Guvenlik.MD5S(mevcutsifre);
                    var veri = db.Kullanici.Where(k => k.ID == id && k.Sifre == mevcutsifre).FirstOrDefault();
                    if (veri != null) { 
                    if(yenisifre == yenisifretekrar)
                    {
                        veri.Sifre = teknikservis.Controllers.Guvenlik.MD5S(yenisifre);
                        veri.Guncellenme = DateTime.Now;
                        db.SaveChanges();
                        bilgi.mesaj = "Şifreniz Güncellendi !";
                    }
                    else
                    {
                        bilgi.mesaj = "Şifreler uyuşmuyor !";
                    }

                    }
                    else
                    {
                        bilgi.mesaj = "Mevcut şifre doğru değil";
                    }
                    
                    
                }
                else
                {
                    bilgi.mesaj = "Ad Soyad ve Mail alanlarını boş bırakmayınız !";
                }

                return Json(bilgi);
            }
            else
            {
                return null;
            }
        }



    }
}
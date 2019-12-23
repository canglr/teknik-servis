using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknikservis.Models;
using TeknikServis.Data.DataContext;
using TeknikServis.Data;
using System.Text;
using teknikservis.Controllers;

namespace teknikservis.Controllers
{
    public class GirisController : Controller
    {
        // GET: Giris
        TeknikContext db = new TeknikContext();
        public ActionResult Index()
        {
            var veri = db.Ayarlar.FirstOrDefault(a => a.ID == 1);
            Session["siteadi"] = veri.Site_Adi;
            return View();
        }

        public class GirisBilgi
        {
            public string adres { get; set; }
            public string mesaj { get; set; }
            public bool islem { get; set; }

        }


       


        [HttpPost]
        public JsonResult Kontrol(string yetkiliadi, string sifre)
        {
            
            GirisBilgi bilgi = new GirisBilgi();
            var kimlik = Guid.NewGuid();
            sifre = Guvenlik.MD5S(sifre);
            var veri = db.Kullanici.FirstOrDefault(k => k.Kullanici_Adi == yetkiliadi && k.Sifre == sifre  && k.Durum == 1);
            if (veri != null)
            {
                Oturumlar oturum = new Oturumlar();
                oturum.Kullanici_ID = veri.ID;
                oturum.Kimlik = kimlik;
                oturum.Ip = Request.UserHostAddress;
                oturum.Durum = 1;
                oturum.Giris_Tarihi = DateTime.Now;
                db.Oturumlar.Add(oturum);
                db.SaveChanges();
                Session["kimlik"] = kimlik;
                Session["id"] = veri.ID;
                Session["Ad"] = veri.Ad;
                Session["Soyad"] = veri.Soyad;
                Session["Grup_ID"] = veri.Grup_ID;
                Session["Mail"] = veri.Mail;
                Session["Gravatar"] = Guvenlik.MD5S(veri.Mail);                
                bilgi.mesaj = @"
                    <audio controls autoplay hidden>
                    <source src=""/cisr/ses/giris.mp3"" type=""audio/mpeg"">                 
                    </audio>
                    <div class=""alert alert-success"" role=""alert"">Yönlendiriliyorsunuz...</div>                
                ";
                bilgi.adres = "/AnaSayfa";
                bilgi.islem = true;

            }
            else
            {
                var veri2 = db.Kullanici.FirstOrDefault(k => k.Kullanici_Adi == yetkiliadi);
                if (veri2 != null)
                {
                    Oturumlar oturum = new Oturumlar();
                    oturum.Kullanici_ID = veri2.ID;
                    oturum.Kimlik = kimlik;
                    oturum.Ip = Request.UserHostAddress;
                    oturum.Durum = 0;
                    oturum.Giris_Tarihi = DateTime.Now;
                    db.Oturumlar.Add(oturum);
                    db.SaveChanges();                    
                    bilgi.mesaj = @"
                    <div class=""alert alert-danger"" role=""alert"">Kullanıcı Adı veya Şifre Yanlış !</div>
                    <audio controls autoplay hidden>
                    <source src=""/cisr/ses/hata.mp3"" type=""audio/mpeg"">                 
                    </audio>

                    ";                    
                }
                else
                {
                    bilgi.mesaj = @"
                    <div class=""alert alert-danger"" role=""alert"">Giriş Başarısız !</div>
                    <audio controls autoplay hidden>
                    <source src=""/cisr/ses/hata.mp3"" type=""audio/mpeg"">                 
                    </audio>
                    ";
                }


            }

           
           return Json(bilgi);
        }

        public ActionResult Cikis()
        {
            Session.RemoveAll();
            Response.Redirect("/Giris");
            return null;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakipOtomasyonu.Models.Entity;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

namespace MVC_StokTakipOtomasyonu.Controllers
{
    [AllowAnonymous]
    public class KullanicilarController : Controller
    {
        MVC_StokTakipOtomasyonuEntities db=new MVC_StokTakipOtomasyonuEntities();
        // GET: Kullanicilar
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]  
        public ActionResult Login(Kullanicilar k)
        {
            var kullanici = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == k.KullaniciAdi && x.Sifre == k.Sifre);
            if (kullanici != null)
            {
                FormsAuthentication.SetAuthCookie(k.KullaniciAdi,false);
                return RedirectToAction("Index","Urunler");


            }
            ViewBag.hata = "Kullanıcı adı veya şifre yanlıştır.";
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        //public ActionResult ResetPassword()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult ResetPassword(Kullanicilar k, string sendMailAdress, string subject, string body)
        //{
        //    var model = db.Kullanicilar.Where(x => x.Email == k.Email).FirstOrDefault();
        //    if (model != null)
        //    { //guid benzersiz keyler üretmeye yarayan sınıf harf sayı karışık bir şekilde üretiyor
        //        Guid rastgele = Guid.NewGuid();
        //        model.Sifre = rastgele.ToString().Substring(0, 8);
        //        db.SaveChanges();
        //        //bir sunuvu belirlicez ve bunun üzerinden maile şifre göndercez. smtp.gmail.com.
        //        SmtpClient client = new SmtpClient(); //587 port 

        //        client.Credentials = new NetworkCredential("testberfin@yandex.com", "13579berfin");
        //        client.Host = "smtp.yandex.ru";
        //        client.Port = 587;
        //        client.EnableSsl = true;
        //        MailMessage mail = new MailMessage();
        //        //nerden nereye aktaracağımızı yani bir mail adresinden diğer mail adresine göndercez
        //        mail.From = new MailAddress("testberfin@yandex.com", "Şifre sıfırlama");
        //        mail.To.Add(model.Email);
        //        mail.IsBodyHtml = true;
        //        mail.Subject = "Şifre Değiştirme İsteği";
        //        mail.Body += "Merhaba " + model.AdiSoyadi + "<br/> Kullanıcı Adınız=" + model.KullaniciAdi + "<br/> Şifreniz=" + model.Sifre;
        //        //NetworkCredential net = new NetworkCredential("testberfinn@gmail.com", "13579berfin");
        //        //client.Credentials = net;
        //        //client.Send(mail);

        //        try
        //        {
        //            client.SendAsync(mail, (Object)mail);
        //            client.Send(mail);
        //            db.SaveChanges();
        //            Response.Redirect("/Kullanicilar/Login");


        //        }
        //        catch (Exception)
        //        {

        //            return RedirectToAction("Login");
        //        }


        //    }
        //    ViewBag.hata = "Böyle bir e-mail adresi bulunamadı.";
        //    return View();
        //}
        [HttpPost]
        public ActionResult ResetPassword(Kullanicilar k)
        {
            var model = db.Kullanicilar.Where(x => x.Email == k.Email).FirstOrDefault();
            if (model != null)
            {
                Guid rastgele = Guid.NewGuid();
                model.Sifre = rastgele.ToString().Substring(0, 8);
                db.SaveChanges();
                SmtpClient client = new SmtpClient("smtp.yandex.com", 587);
                
                client.EnableSsl = true;
                client.Port = 587;
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress("testberfin@yandex.com", "Şifre sıfırlama");
                mail.To.Add(model.Email);
                mail.IsBodyHtml = true; //html taglarını kullanabilmek için
                mail.Subject = "Şifre değiştirme isteği";
                mail.Body += "merhaba" + model.AdiSoyadi + "<br/> kullanıcı adınız=" + model.KullaniciAdi + "<br/> Şifreniz=" + model.Sifre;
                NetworkCredential net = new NetworkCredential("testberfin@yandex.com", "13579berfin");
                client.Credentials = net;

                client.Send(mail);
                return RedirectToAction("Login");


            }
            ViewBag.hata = "böyle bir email yok";
            return View();

        }

        [HttpGet] //sayfaya geçiş yapıyoruz bu yüzden HTTPGET tanımladık
        public ActionResult Kaydol()
        {
            return View();
        }
        [HttpPost] 
        public ActionResult Kaydol(Kullanicilar k)
        {
         if(!ModelState.IsValid) return View();
            
            db.Entry(k).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Guncelle()
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciadi = User.Identity.Name;
                var model = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == kullaniciadi);
                if(model != null)
                {
                    return View(model);
                }
                else
                {
                    return View(new Kullanicilar());
                }
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Guncelle(Kullanicilar k)
        {
            //var kullanici = db.Kullanicilar.Find(k.Id);
            db.Entry(k).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            FormsAuthentication.SignOut();
            /* Eğer ürünler sayfasına gidecekse böyle kullanırız yoksada sace login yazarız.
            FormsAuthentication.SetAuthCookie(k.KullaniciAdi,false);
            return RedirectToAction("Index","Urunler");
            */
            return RedirectToAction("Login", "Kullanicilar");

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakipOtomasyonu.Models.Entity;
using MVC_StokTakipOtomasyonu.MyModel;

namespace MVC_StokTakipOtomasyonu.Controllers
{
    [Authorize(Roles = "A,U")]

    //[ValidateAntiForgeryToken]//url den ekleme işlemini yapmayı önledik
    public class KategorilerController : Controller
    {
        // GET: Kategoriler

        MVC_StokTakipOtomasyonuEntities db = new MVC_StokTakipOtomasyonuEntities();
        public ActionResult Index()
        {
            return View(db.Kategoriler.ToList());
        }

        public ActionResult Ekle() /*birinin post birinin get olması için 2 tane oluşturduk*/
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle2(Kategoriler p)
        {//post işlemini gerçekleştiriyor verileri veritabanına kaydediyor
            if (!ModelState.IsValid) return View("Ekle");
            db.Kategoriler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GuncelleBilgiGetir(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var model = db.Kategoriler.Find(id);
            MyKategoriler k = new MyKategoriler();
            k.ID = model.ID;
            k.Kategori = model.Kategori;
            k.Aciklama = model.Aciklama;
            if (model == null) return HttpNotFound();//Belirtilen ID yoksa hata sayfasına yönlendirecek
            return View(k);
        }
        public ActionResult Guncelle(Kategoriler p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SilBilgiGetir(Kategoriler p)
        {
            var model = db.Kategoriler.Find(p.ID);
            if (model == null) return HttpNotFound();
            return View(model);
        }
        public ActionResult Sil(Kategoriler p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult Markalar(int id)
        {
            var model = db.Markalar.Where(x => x.Kategoriler.ID == id).ToList();
            var kategori = db.Kategoriler.Where(x => x.ID == id).Select(x => x.Kategori).FirstOrDefault();
            ViewBag.viewkategori = kategori;    

            return View(model);

        }
        public ActionResult Urunler(int id)
        {
            var model = db.Urunler.Where(x => x.Kategoriler.ID == id).ToList();
            var kategori = db.Kategoriler.Where(x => x.ID == id).Select(x => x.Kategori).FirstOrDefault();
            ViewBag.viewkategori = kategori;

            return View(model);

        }
    }

}
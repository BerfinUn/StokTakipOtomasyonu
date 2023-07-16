using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_StokTakipOtomasyonu.Models.Entity;
using MVC_StokTakipOtomasyonu.MyModel;

namespace MVC_StokTakipOtomasyonu.Controllers
{
    [Authorize]
    public class UrunlerController : Controller
    {
        // GET: Urunler
        MVC_StokTakipOtomasyonuEntities db = new MVC_StokTakipOtomasyonuEntities();
        public ActionResult Index(string ara)
        {
            var model = db.Urunler.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                model = model.Where(x => x.UrunAdi.Contains(ara) || x.BarkodNo.Contains(ara)).ToList();

            }
            return View("Index", model);
        }

        public ActionResult Index2(string ara)
        {
            var model = db.Urunler.ToList();
            if (!string.IsNullOrEmpty(ara))
            {
                model = model.Where(x => x.UrunAdi.Contains(ara) || x.BarkodNo.Contains(ara)).ToList();

            }
            return View(model);
        }


        private void Yenile(MyUrunler model)
        {
            List<Kategoriler> kategorilist = db.Kategoriler.OrderBy(x => x.Kategori).ToList();
            model.KategoriListesi = (from x in kategorilist
                                     select new SelectListItem
                                     {
                                         Text = x.Kategori,
                                         Value = x.ID.ToString()
                                     }
                ).ToList();

            List<Birimler> birimlist = db.Birimler.OrderBy(x => x.Birim).ToList();
            model.BirimListesi = (from x in birimlist
                                  select new SelectListItem
                                  {
                                      Text = x.Birim,
                                      Value = x.ID.ToString()
                                  }
                ).ToList();
        }

        [HttpGet]//SAYFA ÇAĞIRACAIMIZ KISIM
        public ActionResult Ekle()
        {
            var model = new MyUrunler();
            Yenile(model);
            return View(model);
        }

        
        [HttpPost]//SAYFA ÇALIŞINCA GELECEK KISIM
        public ActionResult Ekle(Urunler p)
        {
            if (!ModelState.IsValid)
            {
                var model = new MyUrunler();
                Yenile(model);
                return View(model);
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index");//işlem yapıldığında anasayfaya gitsin
        }
        [HttpGet]
        public ActionResult MiktarEkle(int id)
        {
            var model = db.Urunler.Find(id);
            return View();
        }
        [HttpPost]
        public ActionResult MiktarEkle(Urunler p)
        {
            var model = db.Urunler.Find(p.ID);
            model.Miktari = model.Miktari + p.Miktari;
            db.SaveChanges();
            return RedirectToAction("Index");
        } 
        [HttpPost]
        public JsonResult GetMarka(int id2)
        {
            var model = new MyUrunler(); //seçilen kategoriye göre marka bilgisi gelecek
            List<Markalar> markaliste = db.Markalar.Where(x => x.KategoriID == id2).OrderBy(x => x.Marka).ToList();
            model.MarkaListesi = (from x in markaliste
                                  select new SelectListItem
                                  {
                                      Text = x.Marka,
                                      Value = x.ID.ToString()
                                  }
            ).ToList();
            model.MarkaListesi.Insert(0, new SelectListItem { Text = "Seçiniz...", Value = "" });
            return Json(model.MarkaListesi, JsonRequestBehavior.AllowGet);

        }
        [HttpGet]
        public ActionResult GuncelleBilgiGetir(int id)
        {
            var model = db.Urunler.Find(id);
            var urun = new MyUrunler();
            urun.ID = model.ID;
            urun.KategoriID = model.KategoriID;
            urun.MarkaID = model.MarkaID;
            urun.UrunAdi = model.UrunAdi;
            urun.BarkodNo = model.BarkodNo;
            urun.AlisFiyati = model.AlisFiyati;
            urun.SatisFiyati = model.SatisFiyati;
            urun.Miktari = model.Miktari;
            urun.KDV = model.KDV;
            urun.BirimID = model.BirimID;
            urun.Tarih = model.Tarih;
            urun.Aciklama = model.Aciklama;
            Yenile(urun);
            List<Markalar> markalist=   db.Markalar.Where(x=>x.KategoriID==model.KategoriID).OrderBy(x=>x.Marka).ToList();
            urun.MarkaListesi = (from x in markalist
                                  select new SelectListItem
                                  {
                                      Text = x.Marka,
                                      Value = x.ID.ToString()
                                  }).ToList();
            return View(urun);
        }
        [HttpPost]
        public ActionResult Guncelle(Urunler p)
        {
            if (!ModelState.IsValid)
            {
                var model = db.Urunler.Find(p.ID);
                var urun = new MyUrunler();
                Yenile(urun);
                List<Markalar> markalist = db.Markalar.Where(x => x.KategoriID == model.KategoriID).OrderBy(x => x.Marka).ToList();
                urun.MarkaListesi = (from x in markalist
                                      select new SelectListItem
                                      {
                                          Text = x.Marka,
                                          Value = x.ID.ToString()
                                      }).ToList();
                return View(urun);
            }
            db.Entry(p).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SilBilgiGetir(Urunler p)
        {
            var model = db.Urunler.Find(p.ID);
            if (model == null) return HttpNotFound();
            return View(model);
        }
        public ActionResult Sil(Urunler p)
        {
            db.Entry(p).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
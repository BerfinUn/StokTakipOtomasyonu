using MVC_StokTakipOtomasyonu.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_StokTakipOtomasyonu.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        MVC_StokTakipOtomasyonuEntities db = new MVC_StokTakipOtomasyonuEntities();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Hakkimizda()
        {
            return View();
        }
        
        public ActionResult Hizmetlerimiz()
        {
            return View();

        }

        public ActionResult Iletisim()
        {
            return View();
             
        }

        public ActionResult Urunler()
        {
            var model = db.Urunler.ToList();
            return View(model);
        }
         
        public ActionResult Kategoriler(int id)
        {
            var model = db.Urunler.Where(x => x.KategoriID == id).ToList();
            var kategori = db.Kategoriler.Where(x => x.ID == id).Select(x => x.Kategori).FirstOrDefault();
            ViewBag.kategori = kategori;
            return View(model);
        }

        public ActionResult Markalar(int id)
        {
            var model = db.Urunler.Where(x => x.MarkaID == id).ToList();
            var marka = db.Markalar.Where(x => x.ID == id).Select(x => x.Marka).FirstOrDefault();
            ViewBag.marka = marka;
            return View(model);
        }
    }
}
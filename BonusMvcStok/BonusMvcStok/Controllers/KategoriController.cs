using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
namespace BonusMvcStok.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        //Modeldeki entities'i çağırarak tablolara ulaşıyoruz.
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Index()
        {
            //kategorileri listeleme için entityframework fonksiyonu
            var kategoriler = db.tblkategori.ToList();
            return View(kategoriler);
        }
        [HttpGet]
        public ActionResult YeniKategori()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniKategori(tblkategori p)
        {
            db.tblkategori.Add(p);
            db.SaveChanges();
            //Kayıt işlemi yapıldıktan sonra Index action'una yönlendir
            return RedirectToAction("Index");
        }

        public ActionResult KategoriSil(int id)
        {
            var ktg = db.tblkategori.Find(id);
            db.tblkategori.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.tblkategori.Find(id);
            return View("KategoriGetir",ktgr);
        }
        
        public ActionResult KategoriGuncelle(tblkategori k)
        {
            var ktg = db.tblkategori.Find(k.id);
            ktg.ad = k.ad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
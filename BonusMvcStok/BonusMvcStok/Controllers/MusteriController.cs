using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace BonusMvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        DbMvcStokEntities db = new DbMvcStokEntities();
        [Authorize]
        public ActionResult Index(int sayfa=1)
        {
            //var musteriListe = db.tblmusteri.ToList();
            //Sayfa numarası kaçtan başlasın ve kaç adet veri olsun
            var musteriListe = db.tblmusteri.Where(x=>x.durum==true).ToList().ToPagedList(sayfa,3);
            return View(musteriListe);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(tblmusteri p)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            p.durum = true;
            db.tblmusteri.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriSil(tblmusteri p)
        {
            var musteriBul = db.tblmusteri.Find(p.id);
            musteriBul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.tblmusteri.Find(id);
            return View("MusteriGetir",mus);
        }

        public ActionResult MusteriGuncelle(tblmusteri t)
        {
            var mus = db.tblmusteri.Find(t.id);
            mus.ad = t.ad;
            mus.soyad = t.soyad;
            mus.sehir = t.sehir;
            mus.bakiye = t.bakiye;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
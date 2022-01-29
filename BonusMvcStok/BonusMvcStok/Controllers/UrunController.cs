using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
namespace BonusMvcStok.Controllers
{
    
    public class UrunController : Controller
    {
        DbMvcStokEntities db = new DbMvcStokEntities();
        // GET: Urun
        public ActionResult Index(string p)
        {
            //Ürün listeleme işlemi ve ürünün true olma durumudur
            //var urunler = db.tblurunler.Where(x=>x.durum==true).ToList();
            var urunler = db.tblurunler.Where(x=>x.durum==true);
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ad.Contains(p) && x.durum==true);
            }
            return View(urunler.ToList());
            
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            
            //Kategorileri dropdownlist aracında göstermek
            List<SelectListItem> ktg = (from x in db.tblkategori.ToList()
                                        select new SelectListItem
                                        {
                                            Text = x.ad,
                                            Value = x.id.ToString()
                                        }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(tblurunler t)
        {
            var ktgr = db.tblkategori.Where(x=>x.id==t.tblkategori.id).FirstOrDefault();
            t.tblkategori = ktgr;
            db.tblurunler.Add(t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Ürün güncelleme
        public ActionResult UrunGetir(int id)
        {
            //Güncellenecek veriyi otomatik olarak dropdownlist'e taşıma
            List<SelectListItem> kat = (from x in db.tblkategori.ToList()
                                         select new SelectListItem
                                         {
                                             Text=x.ad,
                                             Value=x.id.ToString()
                                         }
            ).ToList();
            //ürün güncelleme sayfasına veri taşıma
            var ktgr = db.tblurunler.Find(id);
            //Viewbag ile dropdownlisti View'a taşıyacağız
            ViewBag.urunkategori = kat;
            return View("UrunGetir",ktgr);
        }
        public ActionResult UrunGuncelle(tblurunler p)
        {
            //Ürünü güncelleme işlemi
            var urun = db.tblurunler.Find(p.id);
            urun.marka = p.marka;
            urun.satisfiyat = p.satisfiyat;
            urun.stok = p.stok;
            urun.alisfiyat = p.alisfiyat;
            urun.ad = p.ad;
            var ktg = db.tblkategori.Where(x=>x.id==p.tblkategori.id).FirstOrDefault();
            urun.kategori = ktg.id;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(tblurunler p1)
        {
            var urunbul = db.tblurunler.Find(p1.id);
            urunbul.durum = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
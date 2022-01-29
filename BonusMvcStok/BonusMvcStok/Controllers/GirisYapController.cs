using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BonusMvcStok.Models.Entity;
using System.Web.Security;
namespace BonusMvcStok.Controllers
{
    public class GirisYapController : Controller
    {
        // GET: GirisYap
        DbMvcStokEntities db = new DbMvcStokEntities();
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(tbladmin t)
        {
            var information = db.tbladmin.FirstOrDefault(x => x.kullanici == t.kullanici
            && x.sifre==t.sifre);
            if (information!=null)
            {
                FormsAuthentication.SetAuthCookie(information.kullanici,false);
                return RedirectToAction("Index","Musteri");
            }else
            {
                return View();
            }
            
        }
    }
}
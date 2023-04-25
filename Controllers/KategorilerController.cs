using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class KategorilerController : Controller
    {
        // GET: Kategoriler
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<Kategori> list = r.Kategori.ToList();
            return View(list);
        }

        [MyAuthorization(Roles = "Admin")]
        public ActionResult KategoriEkle()
        {
            Kategori kategori = new Kategori();
            return View(kategori);
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            Kategori kategori = r.Kategori.FirstOrDefault(x => x.KategoriID == k.KategoriID);
            if (kategori==null)
            {
                r.Kategori.Add(k);
            }
            else
            {
                kategori.KategoriID = k.KategoriID;
                kategori.KategoriAd = k.KategoriAd;
            }
            r.SaveChanges();
            return RedirectToAction("index");
        }

       // [MyAuthorization(Roles = "Admin")]
        public void KategoriSil(int id)
        {
            Kategori k = r.Kategori.FirstOrDefault(x => x.KategoriID == id);
            r.Kategori.Remove(k);
            r.SaveChanges();
        }
    }
}
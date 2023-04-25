using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class KampanyalarController : Controller
    {
        // GET: Kampanyalar
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        [HttpGet]
        public ActionResult Index()
        {
            List<Kampanya> list = r.Kampanya.ToList();
            List<Restoran> list2 = r.Restoran.ToList();
            ViewBag.restoran = list2;
            return View(list);
        }
        [MyAuthorization(Roles ="Müşteri")]
        public ActionResult KampanyaliRestoranlar()
        {
            List<Kampanya> list = r.Kampanya.ToList();
            List<Restoran> list2 = r.Restoran.ToList();
            ViewBag.restoran = list2;
            return View(list);
        }
        [MyAuthorization(Roles = "Admin")]
        public ActionResult KampanyaEkle()
        {
            ViewBag.restoran = r.Restoran.ToList();
            Kampanya kampanya = new Kampanya();
            return View(kampanya);
        }
        [HttpPost]
        public ActionResult KampanyaEkle(Kampanya k)
        {
            Kampanya kampanya = r.Kampanya.FirstOrDefault(x => x.KampanyaID == k.KampanyaID);
            if (kampanya==null)
            {
                r.Kampanya.Add(k);
            }
            else
            {
                kampanya.KampanyaAd = k.KampanyaAd;
                kampanya.KampanyaTarihleri = k.KampanyaTarihleri;
                kampanya.RestoranID = k.RestoranID;
            }
            r.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult KampanyaGuncelle(int id)
        {
            Kampanya nesne = r.Kampanya.FirstOrDefault(x => x.KampanyaID == id);
            ViewBag.restoran = r.Restoran.ToList();
            return View("KampanyaEkle", nesne);
        }

        public void KampanyaSil(int id)
        {
            Kampanya k = r.Kampanya.FirstOrDefault(x => x.KampanyaID == id);
            r.Kampanya.Remove(k);
            r.SaveChanges();
        }
    }
}
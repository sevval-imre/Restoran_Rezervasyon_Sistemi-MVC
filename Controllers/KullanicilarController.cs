using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class KullanicilarController : Controller
    {
        // GET: Kullanicilar
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            List<Kullanici> list = r.Kullanici.ToList();
            List<KullaniciRol> liste2 = r.KullaniciRol.ToList();
            ViewBag.rol = liste2;
            return View(list);
        }

        public ActionResult KullaniciEkle()
        {
            ViewBag.rol = r.KullaniciRol.ToList();
            Kullanici kullanici = new Kullanici();
            return View(kullanici);
        }
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici k)
        {
            Kullanici kullanici = r.Kullanici.FirstOrDefault(x => x.KullaniciID == k.KullaniciID);
            if (kullanici==null)
            {
                r.Kullanici.Add(k);
            }
            else
            {
                kullanici.Ad = k.Ad;
                kullanici.Soyad = k.Soyad;
                kullanici.TelefonNo = k.TelefonNo;
                kullanici.Eposta = k.Eposta;
                kullanici.Sifre = k.Sifre;
            }
            r.SaveChanges();
            return RedirectToAction("index");
        }

        public void KullaniciSil(int id)
        {
            Kullanici k = r.Kullanici.FirstOrDefault(x => x.KullaniciID == id);
            r.Kullanici.Remove(k);
            r.SaveChanges();
        }

        public ActionResult KullaniciGuncelle(int id)
        {
            Kullanici nesne = r.Kullanici.FirstOrDefault(x => x.KullaniciID == id);
            ViewBag.rol= r.KullaniciRol.ToList();
            return View("KullaniciEkle", nesne);
        }
    }
}
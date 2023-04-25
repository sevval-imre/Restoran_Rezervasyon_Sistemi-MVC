using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class RezervasyonlarController : Controller
    {
        // GET: Rezervasyonlar
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        [HttpGet]
        public ActionResult Index()
        {
            List<Rezervasyonlar> list = r.Rezervasyonlar.ToList();
            List<Restoran> list2 = r.Restoran.ToList();
            List<Kullanici> list3 = r.Kullanici.ToList();
            List<Masa> list4 = r.Masa.ToList();
            List<Iller> liste5 = r.Iller.ToList();
            List<Ilceler> liste6 = r.Ilceler.ToList();
            ViewBag.restoran = list2;
            ViewBag.kullanici = list3;
            ViewBag.masa = r.Masa.ToList();
            ViewBag.sehir = liste5;
            ViewBag.ilce = liste6;
            return View(list);
        }

        public void RezervasyonSil(int id)
        {
            Rezervasyonlar rz = r.Rezervasyonlar.FirstOrDefault(x => x.RezervasyonID == id);
            r.Rezervasyonlar.Remove(rz);
            r.SaveChanges();
        }
        [MyAuthorization(Roles ="Müşteri")]
        public ActionResult RezervasyonYap()
        {
            ViewBag.restoran = r.Restoran.ToList();
            ViewBag.kullanici = r.Kullanici.ToList();
            ViewBag.masa = r.Masa.ToList();
            ViewBag.sehir = r.Iller.ToList();
            ViewBag.ilce = r.Ilceler.ToList();
            Rezervasyonlar rz = new Rezervasyonlar();
            return View(rz);
        }
        [HttpPost]
        public ActionResult RezervasyonYap(Rezervasyonlar rezervasyonlar)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciAdi = User.Identity.Name;
                var model = r.Kullanici.FirstOrDefault(x => x.Eposta == kullaniciAdi);
                var rz = r.Rezervasyonlar.FirstOrDefault(x => x.KullaniciID == model.KullaniciID && x.RezervasyonID == rezervasyonlar.RezervasyonID);
                if (model != null)
                {
                    if (rz == null)
                    {
                        rezervasyonlar.KullaniciID = model.KullaniciID;
                        r.Rezervasyonlar.Add(rezervasyonlar);
                    }
                    else
                    {
                        rz.MusteriAd = rezervasyonlar.MusteriAd;
                        rz.MusteriSoyad = rezervasyonlar.MusteriSoyad;
                        rz.RezervasyonTarihi = rezervasyonlar.RezervasyonTarihi;
                        rz.MasaID = rezervasyonlar.MasaID;
                        rz.RestoranID = rezervasyonlar.RestoranID;
                    }
                    r.SaveChanges();
                    return RedirectToAction("RezervasyonListesi");
                }
            }
            return HttpNotFound();
        }

        public ActionResult RezervasyonListesi(decimal? rezervasyon)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciAdi = User.Identity.Name;
                var kullanici = r.Kullanici.FirstOrDefault(x => x.Eposta == kullaniciAdi);
                var model = r.Rezervasyonlar.Where(x => x.KullaniciID == kullanici.KullaniciID).ToList();
                return View(model);
            }
            return HttpNotFound();
        }

        public void RezervasyonİptalEt(int id)
        {
            Rezervasyonlar rz = r.Rezervasyonlar.FirstOrDefault(x => x.RezervasyonID == id);
            r.Rezervasyonlar.Remove(rz);
            r.SaveChanges();
        }
    }
}
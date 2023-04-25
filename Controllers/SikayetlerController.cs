using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class SikayetlerController : Controller
    {
        // GET: Sikayetler
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        [HttpGet]
        public ActionResult Index()
        {
            List<Sikayet> list = r.Sikayet.ToList();
            List<Restoran> list2 = r.Restoran.ToList();
            List<Kullanici> list3 = r.Kullanici.ToList();
            ViewBag.restoran = list2;
            ViewBag.kullanici = list3;
            return View(list);
        }
        [MyAuthorization(Roles ="Müşteri")]
        public ActionResult SikayetOlustur()
        {
            ViewBag.restoran = r.Restoran.ToList();
            ViewBag.kullanici = r.Kullanici.ToList();
            Sikayet s = new Sikayet();
            return View(s);
        }
        [HttpPost]
        public ActionResult SikayetOlustur(Sikayet sikayet)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciAdi = User.Identity.Name;
                var model = r.Kullanici.FirstOrDefault(x => x.Eposta == kullaniciAdi);
                var t = r.Sikayet.FirstOrDefault(x => x.KullaniciID == model.KullaniciID && x.SikayetID == sikayet.SikayetID);
                if (model!=null)
                {
                    if (t==null)
                    {
                        sikayet.KullaniciID = model.KullaniciID;
                        r.Sikayet.Add(sikayet);
                    }
                    else
                    {
                        t.MusteriAd = sikayet.MusteriAd;
                        t.MusteriSoyad = sikayet.MusteriSoyad;
                        t.SikayetTarihi = sikayet.SikayetTarihi;
                        t.SikayetNedeni = sikayet.SikayetNedeni;
                        t.RestoranID = sikayet.RestoranID;
                    }
                    r.SaveChanges();
                    return RedirectToAction("SikayetListele");
                }
            }

            return HttpNotFound();
        }

        public ActionResult SikayetListele(decimal? Sikayet)
        {
            if (User.Identity.IsAuthenticated)
            {
                var kullaniciAdi = User.Identity.Name;
                var kullanici = r.Kullanici.FirstOrDefault(x => x.Eposta == kullaniciAdi);
                var model = r.Sikayet.Where(x => x.KullaniciID == kullanici.KullaniciID).ToList();
                return View(model);
            }
            return HttpNotFound();
        }

        public void SikayetSil(int id)
        {
            Sikayet sikayet = r.Sikayet.FirstOrDefault(x => x.SikayetID == id);
            r.Sikayet.Remove(sikayet);
            r.SaveChanges();
        }
    }
}
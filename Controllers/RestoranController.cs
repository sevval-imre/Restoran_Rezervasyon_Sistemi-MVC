using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class RestoranController : Controller
    {
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
         [HttpGet]
         [Authorize(Roles ="Admin")]
        // GET: Restoran

        public ActionResult Index()
        {
            List<Restoran> list = r.Restoran.ToList();
            List<Kategori> liste2 = r.Kategori.ToList();
            List<Iller> liste3 = r.Iller.ToList();
            List<Ilceler> liste4 = r.Ilceler.ToList();
            ViewBag.kategori = liste2;
            ViewBag.sehir = liste3;
            ViewBag.ilce = liste4;
            return View(list);
        }

        public ActionResult RestoranEkle()
        {
            ViewBag.kategori = r.Kategori.ToList();
            ViewBag.sehir = r.Iller.ToList();
            ViewBag.ilce = r.Ilceler.ToList();
            Restoran restoran = new Restoran(); 
            return View(restoran);
        }
        [HttpPost]
        public ActionResult RestoranEkle(Restoran restoran)
        {
            Restoran rs = r.Restoran.FirstOrDefault(x => x.RestoranID == restoran.RestoranID);
            if (rs==null)
            {
                
                r.Restoran.Add(restoran);
            }
            else
            {
                rs.RestoranAd = restoran.RestoranAd;
                rs.TelefonNo = restoran.TelefonNo;
                rs.Adres = restoran.Adres;
                rs.KategoriID = restoran.KategoriID;
                if (Request.Files.Count > 0)
                {
                    string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                    string uzanti = Path.GetExtension(Request.Files[0].FileName);
                    string yol = "~/images/" + dosyaadi + uzanti;
                    Request.Files[0].SaveAs(Server.MapPath(yol));
                    restoran.Resim = "/images/" + dosyaadi + uzanti;
                    rs.Resim = restoran.Resim;
                }
            }
            r.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult RestoranGuncelle(int id)
        {
            Restoran nesne = r.Restoran.FirstOrDefault(x => x.RestoranID == id);
            ViewBag.kategori = r.Kategori.ToList();
            ViewBag.sehir = r.Iller.ToList();
            ViewBag.ilce = r.Ilceler.ToList();
            return View("RestoranEkle",nesne);
        }
       
        public void RestoranSil(int id)
        {
            Restoran res = r.Restoran.FirstOrDefault(x => x.RestoranID == id);
            r.Restoran.Remove(res);
            r.SaveChanges();
        }
    }
}
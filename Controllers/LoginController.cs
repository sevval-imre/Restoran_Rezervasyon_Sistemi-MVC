using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SevvalImre_Proje.Models;

namespace SevvalImre_Proje.Controllers
{
    public class LoginController : Controller
    {
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        // GET: Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Kullanici k)
        {
            RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
            var bilgiler = r.Kullanici.FirstOrDefault(x => x.Eposta == k.Eposta && x.Sifre == k.Sifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Eposta, false);
                Session["Eposta"] = bilgiler.Eposta.ToString();
                Session["Ad"] = bilgiler.Ad.ToString();
                Session["Soyad"] = bilgiler.Soyad.ToString();
                Session["KullaniciID"] = bilgiler.KullaniciID.ToString();
                return RedirectToAction("Index", "AnaSayfa");
            }
            else
            {
                ViewBag.mesaj = "Mail adresi veya şifre hatalı";
                return View();
            }
        }

        public ActionResult LogOut()
        {
            string name = FormsAuthentication.FormsCookieName;
            HttpCookie authcookie = HttpContext.Request.Cookies[name];
            FormsAuthenticationTicket t = FormsAuthentication.Decrypt(authcookie.Value);
            string tname = t.Name;

            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            Kullanici k = new Kullanici();
            return View(k);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(Kullanici kullanici)
        {
            Kullanici k = r.Kullanici.FirstOrDefault(x => x.KullaniciID == kullanici.KullaniciID);
            r.Kullanici.Add(kullanici);
            r.SaveChanges();
            return RedirectToAction("Login");
        }

        public ActionResult Hata()
        {
            return View();
        }
    }
}
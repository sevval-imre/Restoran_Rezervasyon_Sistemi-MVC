using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SevvalImre_Proje.Models;
using SevvalImre_Proje.Security;


namespace SevvalImre_Proje.Controllers
{
    public class AnaSayfaController : Controller
    {
        RestoranRezervasyonEntities r = new RestoranRezervasyonEntities();
        // GET: AnaSayfa
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Restoran> list = r.Restoran.ToList();
            return View(list);
        }       
    }
}
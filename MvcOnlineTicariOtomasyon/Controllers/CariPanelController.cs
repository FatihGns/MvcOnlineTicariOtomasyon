using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class CariPanelController : Controller
    {

        // GET: CariPanel
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
      [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = _context.Messages.Where(x => x.Alici == mail).ToList();
            ViewBag.m = mail;
            var mailid=_context.Carilers.Where(x=>x.CariMail== mail).Select(y=>y.CariID).FirstOrDefault();
            ViewBag.m1 = mailid;
            var toplamsatis = _context.SatisHarekets.Where(x => x.CariID == mailid).Count();
            ViewBag.toplamsatis = toplamsatis;
            var toplamtutar=_context.SatisHarekets.Where(x=>x.CariID==mailid).Sum(y=> (decimal?)y.ToplamTutar);
            ViewBag.toplamtutar = toplamtutar;
            var toplamürünsayisi = _context.SatisHarekets.Where(x => x.CariID == mailid).Sum(y => (decimal?)y.Adet);
            ViewBag.toplamürünsayisi = toplamürünsayisi;
            var adsoyad = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariName + " " + y.CariSurname).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;
            var cariMail = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariMail).FirstOrDefault();
            ViewBag.cariMail = cariMail;
            return View(degerler);
        }
        public ActionResult Siparişlerim()
        {
            var mail = (string)Session["CariMail"];
            var id = _context.Carilers.Where(x => x.CariMail == mail.ToString()).Select
                (y => y.CariID).FirstOrDefault();
            var degerler = _context.SatisHarekets.Where(x => x.CariID == id).ToList();
            ViewBag.m = mail;
            return View(degerler);
        }
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = _context.Messages.Where(x => x.Alici == mail).OrderByDescending(x=>x.MesajID).ToList();
            var gelensayisi = _context.Messages.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = _context.Messages.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);

        }
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = _context.Messages.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajID).ToList();
            var gelensayisi = _context.Messages.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = _context.Messages.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View(mesajlar);
        }
        public ActionResult MesajDetay(int id)
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = _context.Messages.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = _context.Messages.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            var gönderenKonu = _context.Messages.Where(x => x.MesajID == id).Select(y => y.Konu).FirstOrDefault();
            ViewBag.d3 = gönderenKonu;
            var gönderenIcerik = _context.Messages.Where(x => x.MesajID == id).Select(y => y.Icerik).FirstOrDefault();
            ViewBag.d4 = gönderenIcerik;
            var gönderenMail = _context.Messages.Where(x => x.MesajID == id).Select(y => y.Gönderici).FirstOrDefault();
            ViewBag.d5 = gönderenMail;
            var gönderentarih = _context.Messages.Where(x => x.MesajID == id).Select(y => y.Tarih).FirstOrDefault();
            ViewBag.d6 = gönderentarih.ToShortDateString();
            return View();
        }
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gelensayisi = _context.Messages.Count(x => x.Alici == mail).ToString();
            ViewBag.d1 = gelensayisi;
            var gidensayisi = _context.Messages.Count(x => x.Gönderici == mail).ToString();
            ViewBag.d2 = gidensayisi;
            return View();
        }
        [HttpPost]
        public ActionResult YeniMesaj(Message m)
        {
            var mail = (string)Session["CariMail"];
            m.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            m.Gönderici = mail;
            _context.Messages.Add(m);
            _context.SaveChanges();
            return View();
        }
        public ActionResult KargoTakip(string p)
        {
            var kargo = from x in _context.KargoDetays select x;
            kargo = kargo.Where(y => y.TakipKodu.Contains(p));
            return View(kargo.ToList());
        }
        public ActionResult CariKargoTakip(string id)
        {
            var degerler=_context.KargoTakips.Where(x=>x.TakipKodu==id).ToList();
            return View(degerler);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // tüm session bilgilerini temizler
            return RedirectToAction("Index", "Login"); // giriş sayfasına yönlendir
        }
        public PartialViewResult Settings()
        {
            var mail = (string)Session["CariMail"];
            var id = _context.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariID).FirstOrDefault();
            var caribul = _context.Carilers.Find(id);
            return PartialView("Settings", caribul);
        }
        public PartialViewResult Duyurular()
        {
            var veriler = _context.Messages.Where(x => x.Gönderici == "admin@gmail.com").ToList();
            return PartialView(veriler);
        }
        public ActionResult CariBilgiGüncelle(Cariler cr)
        {
            var cari = _context.Carilers.Find(cr.CariID);
            cari.CariName = cr.CariName;
            cari.CariSurname= cr.CariSurname;
            cari.CariMail = cr.CariMail;
            cari.CariSehir= cr.CariSehir;
            cari.CariSifre= cr.CariSifre;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult Chat()
        {
            return View();
        }
    }
}
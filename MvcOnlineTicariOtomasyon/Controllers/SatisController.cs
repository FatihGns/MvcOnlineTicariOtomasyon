using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var degerler = _context.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CreateSatis()
        {
            List<SelectListItem> deger1=(from x in _context.Products.ToList()
                                         select new SelectListItem
                                         {
                                                Text = x.ProductName,
                                                Value = x.ProductID.ToString()
                                         }).ToList();

            List<SelectListItem> deger2 = (from x in _context.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariName + " " + x.CariSurname,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in _context.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult CreateSatis(SatisHareket satisHareket)
        {
           
                satisHareket.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                _context.SatisHarekets.Add(satisHareket);
                _context.SaveChanges();
                return RedirectToAction("Index");
        }
        public ActionResult SatisGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in _context.Products.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.ProductName,
                                               Value = x.ProductID.ToString()
                                           }).ToList();

            List<SelectListItem> deger2 = (from x in _context.Carilers.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CariName + " " + x.CariSurname,
                                               Value = x.CariID.ToString()
                                           }).ToList();
            List<SelectListItem> deger3 = (from x in _context.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr1 = deger1;

            var deger = _context.SatisHarekets.Find(id);
            return View("SatisGetir", deger);
        }
        public ActionResult SatisGuncelle(SatisHareket satisHareket)
        {
            if (ModelState.IsValid)
            {
                var deger = _context.SatisHarekets.Find(satisHareket.SatisID);
                deger.CariID = satisHareket.CariID;
                deger.EmployeeID = satisHareket.EmployeeID;
                deger.ProductID = satisHareket.ProductID;
                deger.Adet = satisHareket.Adet;
                deger.Fiyat = satisHareket.Fiyat;
                deger.ToplamTutar = satisHareket.ToplamTutar;
                deger.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("SatisGetir", satisHareket);
        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
        public ActionResult SatisPdf(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.SatisID == id).ToList();
            return View(degerler);
        }
    }
}
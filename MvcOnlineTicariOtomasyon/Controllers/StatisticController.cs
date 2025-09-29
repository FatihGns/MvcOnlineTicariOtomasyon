using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class StatisticController : Controller
    {
        // GET: Statistic
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var deger1 = _context.Carilers.Count().ToString();
            ViewBag.d1 = deger1;
            var deger2 = _context.Products.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3= _context.Employees.Count().ToString();
            ViewBag.d3=deger3;
            var deger4=_context.Categories.Count().ToString();
            ViewBag.d4 = deger4;
            var deger5=_context.Products.Select(x=>(int)x.Stock).Sum().ToString();
            ViewBag.d5 = deger5;
            var deger6 = _context.Products.GroupBy(x => x.Brand).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d6 = deger6;
            var deger7=_context.Products.Where(x=>x.Stock<=20).Count().ToString();
            ViewBag.d7 = deger7;
            var deger8=(from x in _context.Products orderby x.SalePrice descending select x.ProductName).FirstOrDefault();
            ViewBag.d8 = deger8;
            var deger9 = (from x in _context.Products orderby x.SalePrice ascending select x.ProductName).FirstOrDefault();
            ViewBag.d9 = deger9;
            var deger10 = _context.Products.Select(x=>x.Brand).Distinct().Count().ToString();
            ViewBag.d10 = deger10;
            var deger11 = _context.Products.Count(x => x.CategoryID == 1).ToString();
            ViewBag.d11 = deger11;
            var deger12 = _context.Products.Count(x => x.CategoryID == 7).ToString();
            ViewBag.d12 = deger12;
            var deger13 = _context.Products.Where(u => u.ProductID ==
            (_context.SatisHarekets.GroupBy(x => x.ProductID).OrderByDescending(z => z.Count()).Select(x => x.Key).FirstOrDefault())).Select(k => k.ProductName).FirstOrDefault();
            ViewBag.d13 = deger13;
            var deger14 = _context.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;
            DateTime bugün = DateTime.Today;
            var deger15 = _context.SatisHarekets.Count(x => x.Tarih== bugün).ToString();
            ViewBag.d15 = deger15;
            var deger16 = _context.SatisHarekets.Where(x => x.Tarih == bugün).Sum(x => (decimal?)x.ToplamTutar) ?? 0;
            ViewBag.d16 = deger16;

            //var deger16 = _context.SatisHarekets.Where(x => x.Tarih == bugün).Sum(x => x.ToplamTutar).ToString();
            //ViewBag.d16 = deger16;

            return View();
        }
        public ActionResult SimpleTables()
        {
            var sorgu = from x in _context.Carilers
                           group x by x.CariSehir into g
                           select new EntitesGroup
                           {
                               Sehir = g.Key,
                               Sayı = g.Count()
                           };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu = from x in _context.Employees
                        group x by new { x.DepartmentID, x.Department.DepartmentName } into g
                        select new EntitesGroup2
                        {
                            DepartmanName = g.Key.DepartmentName, // buraya map ediyoruz
                            Sayi = g.Count()
                        };
            return PartialView(sorgu.ToList());
        }
        public PartialViewResult Partial2()
        {
            var sorgu = _context.Carilers.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial3()
        {
            var sorgu = _context.Products.ToList();
            return PartialView(sorgu);
        }
        public PartialViewResult Partial4()
        {
            var sorgu = from x in _context.Products
                        group x by x.Brand into g
                        select new EntitiesGroup3
                        {
                            Brand = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(sorgu.ToList());
        }
    }
}
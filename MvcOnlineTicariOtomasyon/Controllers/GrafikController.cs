
using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        MvcTicariOtomasyonContext _context=new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600,600);
            grafikciz.AddTitle("Kategoriler ve Ürün Sayıları").AddLegend("Stok")
                .AddSeries("Degerler", xValue: new[] { "Mobilya", "Ofis Eşyaları", "Koltuk" }, yValues: new[] { 500, 300, 750 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index3()
        {
            ArrayList xvalue=new ArrayList();  
            ArrayList yvalue=new ArrayList();  
            var sonuclar=_context.Products.ToList();
            sonuclar.ToList().ForEach(x=>xvalue.Add(x.ProductName));
            sonuclar.ToList().ForEach(y=>yvalue.Add(y.Stock));
            var grafik=new Chart(600,600);
            grafik.AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index4()
        {

            return View();
        }
        public ActionResult VisulazieProductResult()
        {
            return Json(Productlist(), JsonRequestBehavior.AllowGet);
        }

        public List<sinif1> Productlist()
        {
            List<sinif1> snf=new List<sinif1>();
            snf.Add(new sinif1()
            {
                ProductName = "Bilgisayar",
                Stock=120,
            });
            snf.Add(new sinif1()
            {
                ProductName = "Telefon",
                Stock = 150,
            });
            snf.Add(new sinif1()
            {
                ProductName = "Beyaz Eşya",
                Stock = 400,
            });
            snf.Add(new sinif1()
            {
                ProductName = "Mobilya",
                Stock = 250,
            });
            snf.Add(new sinif1()
            {
                ProductName = "Küçük Ev Aletleri",
                Stock = 160,
            });
            return snf;
        }

        public ActionResult Index5()
        {

            return View();
        }
        public ActionResult VisulazieProductResult2()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif2> Urunlistesi()
        {
            List<sinif2> snf = new List<sinif2>();

            using (var c=new MvcTicariOtomasyonContext())
            {
                snf = c.Products.Select(x => new sinif2
                {
                    ProductName = x.ProductName,
                     Stock=x.Stock,
                }).ToList();
            }
            return snf;     
        }
        public ActionResult Index6()
        {

            return View();
        }
        public ActionResult VisulazieProductResult3()
        {
            return Json(Urunlistesi2(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif2> Urunlistesi2()
        {
            List<sinif2> snf = new List<sinif2>();

            using (var c = new MvcTicariOtomasyonContext())
            {
                snf = c.Products.Select(x => new sinif2
                {
                    ProductName = x.ProductName,
                    Stock = x.Stock,
                }).ToList();
            }
            return snf;
        }
        public ActionResult Index7()
        {

            return View();
        }
        public ActionResult VisulazieProductResult4()
        {
            return Json(Urunlistesi3(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif2> Urunlistesi3()
        {
            List<sinif2> snf = new List<sinif2>();

            using (var c = new MvcTicariOtomasyonContext())
            {
                snf = c.Products.Select(x => new sinif2
                {
                    ProductName = x.ProductName,
                    Stock = x.Stock,
                }).ToList();
            }
            return snf;
        }
    }
}
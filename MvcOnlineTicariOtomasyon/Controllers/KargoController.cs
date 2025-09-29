using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        // GET: Kargo

        MvcTicariOtomasyonContext _context=new MvcTicariOtomasyonContext();
        public ActionResult Index(string p)
        {
            var kargo = from x in _context.KargoDetays select x;
            if (!string.IsNullOrEmpty(p))
            {
                kargo = kargo.Where(y => y.TakipKodu.Contains(p));
               
            }
            return View(kargo.ToList());
        }
        [HttpGet]
        public ActionResult CreateKargo()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H","K"};
            int k1, k2, k3;
            k1=rnd.Next(0, karakterler.Length-1);
            k2=rnd.Next(0, karakterler.Length - 1);
            k3=rnd.Next(0, karakterler.Length - 1);
            int s1,s2,s3;
            s1=rnd.Next(100,1000);//10 
            s2=rnd.Next(10,99);
            s3=rnd.Next(10,99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.TakipKod = kod;


            List<SelectListItem> deger3 = (from x in _context.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult CreateKargo(KargoDetay k)
        {
           
            _context.KargoDetays.Add(k);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string id)
        {
            var degerler = _context.KargoTakips.Where(x => x.TakipKodu == id).ToList();
            return View(degerler);
        }
    }
}
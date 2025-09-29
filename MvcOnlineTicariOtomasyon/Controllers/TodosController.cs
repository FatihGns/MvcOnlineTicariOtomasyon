using MvcOnlineTicariOtomasyon.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class TodosController : Controller
    {
        // GET: Todos
        MvcTicariOtomasyonContext _context=new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
           var deger1=_context.Carilers.Count().ToString(); 
           ViewBag.d1=deger1;
            var deger2 = _context.Products.Count().ToString();
            ViewBag.d2 = deger2;
            var deger3=_context.Categories.Count().ToString();
            ViewBag.d3 = deger3;
            var deger4 = _context.Carilers.Select(x=>x.CariSehir).Distinct().Count().ToString();
            ViewBag.d4 = deger4;
            var sehirler = _context.Carilers
                       .GroupBy(x => x.CariSehir)
                       .Select(g => new
                       {
                           Sehir = g.Key,
                           Sayisi = g.Count()
                       })
                       .ToList();

            ViewBag.Sehirler = sehirler.Distinct().Select(x => x.Sehir).ToList();
            ViewBag.Sayilar = sehirler.Select(x => x.Sayisi).ToList();


            var todolist=_context.TodoLists.ToList();
         
            return View(todolist);
        }
    }
}
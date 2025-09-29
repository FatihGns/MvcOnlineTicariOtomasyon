using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CarilerController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var cariler = _context.Carilers.Where(x=>x.Status==true).ToList();
            return View(cariler);
        }
        [HttpGet]
        public ActionResult CreateCariler()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCariler(Cariler cariler)
        {

            if (ModelState.IsValid)
            {
                cariler.Status = true; 
                _context.Carilers.Add(cariler);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cariler);
        }
        public ActionResult DeleteCariler(int id)
        {
            var cariler = _context.Carilers.Find(id);

            if (cariler != null)
            {
                cariler.Status = false;
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Cari Durum False Oldu.";

            }
            else
            {
                TempData["SuccessMessage"] = "Cari Durum True Oldu.";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateCariler(int id)
        {
            var cariler = _context.Carilers.Find(id);
            if (cariler == null)
            {
                TempData["ToastMessage"] = "Cari bulunamadı.";
                TempData["ToastType"] = "danger";
                return RedirectToAction("Index");
            }

            return View(cariler);
        }
        [HttpPost]
        public ActionResult UpdateCariler(Cariler cariler)
        {
            if(!ModelState.IsValid)
            {
                return View(cariler);
            }

            var updateCariler = _context.Carilers.Find(cariler.CariID);
            if (updateCariler != null)
            {
                updateCariler.CariName =cariler.CariName;
                updateCariler.CariSurname = cariler.CariSurname;
                updateCariler.CariSehir = cariler.CariSehir;
                updateCariler.CariMail = cariler.CariMail;
                _context.SaveChanges();

                TempData["ToastMessage"] = "Cari başarıyla güncellendi.";

            }
            else
            {
                TempData["ToastMessage"] = "Cari bulunamadı.";
            }

            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.CariID == id).ToList();
            var cr = _context.Carilers.Where(x => x.CariID == id).Select(y => y.CariName + " " + y.CariSurname).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }
    }
}

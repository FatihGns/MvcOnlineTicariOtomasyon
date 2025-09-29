using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "A")]
    public class DepartmanController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var values = _context.Departments.Where(x => x.Status == true).ToList();
            return View(values);
        }
      

        [HttpGet]
        public ActionResult CreateDepartman()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDepartman(Department department)
        {

            if (ModelState.IsValid)
            {
                department.Status = true;
                _context.Departments.Add(department);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public ActionResult DeleteDepartman(int id)
        {
            var Departman = _context.Departments.Find(id);

            if (Departman != null)
            {
                Departman.Status = false;
                _context.SaveChanges();
                TempData["ToastMessage"] = "Departman Durumu False Olarak Değiştirildi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Departman bulunamadı.";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateDepartman(int id)
        {
            var departman = _context.Departments.Find(id);
            if (departman == null)
            {
                TempData["ToastMessage"] = "Departman bulunamadı.";
                TempData["ToastType"] = "danger";
                return RedirectToAction("Index");
            }
            return View(departman);
        }
        [HttpPost]
        public ActionResult UpdateDepartman(Department department)
        {
            var updatedDepartman = _context.Departments.Find(department.DepartmentID);
            if (updatedDepartman != null)
            {
                updatedDepartman.DepartmentName = department.DepartmentName;
                _context.SaveChanges();
                TempData["ToastMessage"] = "Departman başarıyla güncellendi.";
            }
            else
            {
                TempData["ToastMessage"] = "Departman bulunamadı.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult DepartmanDetail(int id)
        {
            var degerler = _context.Employees.Where(x => x.DepartmentID == id).ToList();
            var dpt = _context.Departments.Where(x => x.DepartmentID == id).Select(y => y.DepartmentName).FirstOrDefault();
            if (degerler == null || !degerler.Any())
            {
                TempData["ToastMessage"] = "Bu departmanda çalışan bulunamadı.";
                TempData["ToastType"] = "warning";
                return RedirectToAction("Index");
            }
            
            ViewBag.departman = dpt;
            return View(degerler);
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var degerler = _context.SatisHarekets.Where(x => x.EmployeeID == id).ToList();
            var dpt = _context.SatisHarekets.Where(x => x.EmployeeID == id).Select(y => y.Employees.EmployeeName + " " + y.Employees.EmployeeSurname).FirstOrDefault();
            ViewBag.dper = dpt;
            return View(degerler);
        }
    }
}
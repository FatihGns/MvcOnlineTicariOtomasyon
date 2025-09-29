using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class EmployeeController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var degerler = _context.Employees.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CreateEmployee()
        {
            ViewBag.Departments = _context.Departments.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult CreateEmployee(Employee employee)
        {
                if(Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/"+dosyaadi+uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                employee.EmployeeImage = "/Image/" + dosyaadi + uzanti;
            }
                _context.Employees.Add(employee);
                _context.SaveChanges();
                return RedirectToAction("Index");

        }
        public ActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Çalışan bulunamadı.";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateEmployee(int id)
        {
            var employee = _context.Employees.Find(id);
            
            if (employee == null)
            {
                TempData["ToastMessage"] = "Çalışan bulunamadı.";
                TempData["ToastType"] = "danger";
                return RedirectToAction("Index");
            }
            ViewBag.Departments1 = _context.Departments.ToList();
            return View(employee);
        }
        [HttpPost]
        public ActionResult UpdateEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                employee.EmployeeImage = "/Image/" + dosyaadi + uzanti;
            }
            var updateEmployee = _context.Employees.Find(employee.EmployeeID);
            if (updateEmployee != null)
            {
                updateEmployee.EmployeeName = employee.EmployeeName;
                updateEmployee.EmployeeSurname = employee.EmployeeSurname;
                updateEmployee.DepartmentID = employee.DepartmentID;
                updateEmployee.EmployeeImage = employee.EmployeeImage;
                _context.SaveChanges();
                TempData["ToastMessage"] = "Çalışan başarıyla güncellendi.";
            }
            else
            {
                TempData["ToastMessage"] = "Çalışan bulunamadı.";
            }
            return RedirectToAction("Index");
        }
        public ActionResult EmployeeDetail()
        {
            var degerler = _context.Employees.ToList();
            return View(degerler);
        }
    }
}
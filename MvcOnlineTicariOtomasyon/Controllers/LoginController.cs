using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login
        MvcTicariOtomasyonContext _context=new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CariLogin()
        {

            return View();
        }
        [HttpPost]
        public ActionResult CariLogin(Cariler c)
        {
            var bilgiler = _context.Carilers.FirstOrDefault(x => x.CariMail == c.CariMail && x.CariSifre==c.CariSifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"]=bilgiler.CariMail.ToString();
                return RedirectToAction("Index", "CariPanel");
            }
            else
            {
                return View();
            }
               
        }
        [HttpGet]
        public ActionResult Employeelogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Employeelogin(Employee e)
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult CariRegister()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult CariRegister(Cariler c)
        {
            _context.Carilers.Add(c);
            _context.SaveChanges();

            return PartialView();
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin a)
        {
            var bilgier = _context.Admins.FirstOrDefault(x=>x.Username==a.Username && x.Password==a.Password);
            if (bilgier != null)
            {
                FormsAuthentication.SetAuthCookie(bilgier.Username, false);
                Session["UserName"]=bilgier.Username.ToString();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}
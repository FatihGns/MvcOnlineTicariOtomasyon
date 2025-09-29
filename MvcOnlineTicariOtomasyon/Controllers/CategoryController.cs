using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList.Mvc;
using PagedList;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index(int sayfa=1)
        {
            var values = _context.Categories.ToList().ToPagedList(sayfa, 5);
            return View(values);
        }
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Kategori başarıyla Silindi.";

            }
            else
            {
                TempData["ErrorMessage"] = "Kategori bulunamadı.";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                TempData["ToastMessage"] = "Kategori bulunamadı.";
                TempData["ToastType"] = "danger";
                return RedirectToAction("Index");
            }

            return View(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            var updateCategory = _context.Categories.Find(category.CategoryID);
            if (updateCategory != null)
            {
                updateCategory.CategoryName = category.CategoryName;
                _context.SaveChanges();

                TempData["ToastMessage"] = "Kategori başarıyla güncellendi.";
            
            }
            else
            {
                TempData["ToastMessage"] = "Kategori bulunamadı.";
            }

            return RedirectToAction("Index");
        }

        public ActionResult Deneme()
        {
            Cascading cs = new Cascading();
            cs.Categories = new SelectList(_context.Categories, "CategoryID", "CategoryName");
            cs.Products = new SelectList(_context.Products, "ProductID", "ProductName");
            return View(cs);    
            
        }
        public JsonResult GetProduct(int p)
        {
            var productlist = (from x in _context.Products
                               join y in _context.Categories
                               on x.Category.CategoryID equals y.CategoryID
                               where x.CategoryID == p
                               select new
                               {
                                   Text = x.ProductName,
                                   Value = x.ProductID.ToString()
                               }).ToList();
            return Json(productlist,JsonRequestBehavior.AllowGet);
        }
    }
}

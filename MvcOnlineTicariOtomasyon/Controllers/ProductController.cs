using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductController : Controller
    {
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index(string p)
        {
            
            var urunler=from x in _context.Products select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.ProductName.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            List<SelectListItem> deger1 = (from x in _context.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ActionResult DeleteProduct(int id)
        {
            var product= _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                TempData["ToastMessage"] = "Ürün başarıyla silindi.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["ErrorMessage"] = "Kategori bulunamadı.";
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UpdateProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                TempData["ToastMessage"] = "Ürün bulunamadı.";
                TempData["ToastType"] = "danger";
                return RedirectToAction("Index");
            }
            List<SelectListItem> deger2 = (from x in _context.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.dgr2 = deger2;
            return View(product);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            var updatedProduct = _context.Products.Find(product.ProductID);
            if (updatedProduct != null)
            {
                updatedProduct.ProductName = product.ProductName;
                updatedProduct.Brand = product.Brand;
                updatedProduct.Stock = product.Stock;
                updatedProduct.PurchasePrice = product.PurchasePrice;
                updatedProduct.SalePrice = product.SalePrice;
                updatedProduct.Status = product.Status;
                updatedProduct.ProductImage = product.ProductImage;
                updatedProduct.CategoryID =product.CategoryID;
                _context.SaveChanges();
                TempData["ToastMessage"] = "Ürün başarıyla güncellendi.";
            }
            else
            {
                TempData["ToastMessage"] = "Ürün bulunamadı.";
            }

            return RedirectToAction("Index");
        }
        public ActionResult ProductList()
        {
            var values = _context.Products.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            List<SelectListItem> deger3 = (from x in _context.Employees.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.EmployeeName + " " + x.EmployeeSurname,
                                               Value = x.EmployeeID.ToString()
                                           }).ToList();
            ViewBag.dgr3 = deger3;
            var deger2 = _context.Products.Find(id);
            ViewBag.dgr2 = deger2.ProductID;
            ViewBag.dgr1 = deger2.SalePrice;


            return View();
        }
        [HttpPost]
        public ActionResult SatisYap(SatisHareket p)
        {

            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            _context.SatisHarekets.Add(p);
            _context.SaveChanges();
            return RedirectToAction("Index","Satis");
        }

    }

}
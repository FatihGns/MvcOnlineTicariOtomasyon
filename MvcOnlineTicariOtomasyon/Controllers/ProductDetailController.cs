using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class ProductDetailController : Controller
    {
        // GET: ProductDetail
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        { 
            DetailProduct detailProduct = new DetailProduct();

            //var degerler = _context.Products.Where(x=>x.ProductID==4).ToList();
            detailProduct.Deger1 = _context.Products.Where(x => x.ProductID == 4).ToList();
            detailProduct.Deger2 = _context.Details.Where(x => x.DetailID == 1).ToList();
            return View(detailProduct);
        }
    }
}
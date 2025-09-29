using MvcOnlineTicariOtomasyon.Models.Context;
using MvcOnlineTicariOtomasyon.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        // GET: Fatura
        MvcTicariOtomasyonContext _context = new MvcTicariOtomasyonContext();
        public ActionResult Index()
        {
            var liste=_context.Faturalars.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult CreateFatura()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateFatura(Faturalar f)
        {
            _context.Faturalars.Add(f);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetFatura(int id)
        {
            var fatura = _context.Faturalars.Find(id);
            return View("GetFatura",fatura);

        }
        public ActionResult UpdateFatura(Faturalar f)
        {
            var fatura=_context.Faturalars.Find(f.FaturaID);
            fatura.FaturaSeriNo = f.FaturaSeriNo;
            fatura.FaturaSıraNo=f.FaturaSıraNo;
            fatura.VergiDairesi=f.VergiDairesi;
            fatura.Tarih=f.Tarih;
            fatura.Saat=f.Saat;
            fatura.TeslimAlan=f.TeslimAlan;
            fatura.TeslimEden=f.TeslimEden;
            fatura.Toplam=f.Toplam;
            _context.SaveChanges();
            return RedirectToAction("Index");

        }
        public ActionResult FaturaDetail(int id)
        {
            var degerler = _context.FaturaKalems.Where(x=>x.FaturaID==id).ToList();
            return View(degerler);

        }
        [HttpGet]
        public ActionResult CreateFaturaKalem()
        {
            return View();
        }
        public ActionResult CreateFaturaKalem(FaturaKalem f)
        {
             _context.FaturaKalems.Add(f);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Dinamik()
        {
            Dinamik dinamik=new Dinamik();
            dinamik.deger1=_context.Faturalars.ToList();
            dinamik.deger2=_context.FaturaKalems.ToList();
            return View(dinamik);
        }
        public ActionResult FaturaKaydet(string FaturaSeriNo,string FaturaSıraNo,
            DateTime Tarih,string VergiDairesi,string Saat,string TeslimEden,string TeslimAlan,string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar faturalar = new Faturalar();
            faturalar.FaturaSıraNo = FaturaSıraNo;
            faturalar.FaturaSeriNo = FaturaSeriNo;
            faturalar.Tarih = Tarih;
            faturalar.VergiDairesi = VergiDairesi;
            faturalar.Saat = Saat;
            faturalar.TeslimEden = TeslimEden;
            faturalar.TeslimAlan = TeslimAlan;
            faturalar.Toplam = decimal.Parse(Toplam);
            _context.Faturalars.Add(faturalar);
     
            foreach (var item in kalemler)
            {
                FaturaKalem fk=new FaturaKalem();
                fk.Aciklama=item.Aciklama;
                fk.BirimFiyat=item.BirimFiyat;
                fk.FaturaID = item.FaturaID;
                fk.Miktar=item.Miktar;
                fk.Tutar=item.Tutar;
                _context.FaturaKalems.Add(fk);
            }
            _context.SaveChanges();
            return Json("İşlem Başarılı",JsonRequestBehavior.AllowGet);

        }
    }
}
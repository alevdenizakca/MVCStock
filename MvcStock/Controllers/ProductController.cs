using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class ProductController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities();
        // GET: ProductDefault
        public ActionResult Index()
        {
            var deger = db.TURUNLERs.ToList();
            return View(deger);
        }
        public ActionResult Create()
        {
            List<SelectListItem> degerler = (from i in db.TKATEGORILERs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORIAD,
                                                 Value = i.KATEGORIID.ToString()
                                             }).ToList();
            ViewBag.degerler = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult Create(TURUNLER p1)
        {
            var category=db.TKATEGORILERs.Where(m=>m.KATEGORIID==p1.TKATEGORILER.KATEGORIID).FirstOrDefault();
            p1.TKATEGORILER = category;
            db.TURUNLERs.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var product = db.TURUNLERs.Find(id);
            db.TURUNLERs.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringProduct(int id)
        {
            var product = db.TURUNLERs.Find(id);
            List<SelectListItem> degerler = (from i in db.TKATEGORILERs.ToList() select new SelectListItem
            {
                Text= i.KATEGORIAD,
                Value = i.KATEGORIID.ToString()
            }).ToList();
            ViewBag.degerler = degerler;
            return View(product);
        }
        public ActionResult Update(TURUNLER p1)
        {
            var product = db.TURUNLERs.Find(p1.URUNID);
            product.URUNAD = p1.URUNAD;
            product.MARKA = p1.MARKA;

            var category = db.TKATEGORILERs.Where(m=>m.KATEGORIID==p1.TKATEGORILER.KATEGORIID).FirstOrDefault();

            product.URUNKATEGORI = category.KATEGORIID;
            product.FIYAT = p1.FIYAT;
            product.STOK= p1.STOK;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
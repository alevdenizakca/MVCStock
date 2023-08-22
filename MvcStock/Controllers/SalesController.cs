using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class SalesController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities();
        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.TSATISLARs.ToList();
            return View(sales);
        }

        public ActionResult Create()
        {
            List<SelectListItem> products = (from i in db.TURUNLERs.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.URUNAD,
                                                 Value = i.URUNID.ToString()
                                             }).ToList();
            ViewBag.products = products;
            List<SelectListItem> customers = (from i in db.TMUSTERILERs.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = i.MUSTERIAD + " " + i.MUSTERISOYAD,
                                                  Value = i.MUSTERIID.ToString(),
                                              }).ToList();
            ViewBag.customers = customers;
            return View();
        }
        [HttpPost]
        public ActionResult Create(TSATISLAR p1)
        {
            
            var customer = db.TMUSTERILERs.Where(m=>m.MUSTERIID==p1.TMUSTERILER.MUSTERIID).FirstOrDefault();
            var product = db.TURUNLERs.Where(m=>m.URUNID==p1.TURUNLER.URUNID).FirstOrDefault();
            p1.TMUSTERILER = customer;
            p1.TURUNLER = product;
            db.TSATISLARs.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
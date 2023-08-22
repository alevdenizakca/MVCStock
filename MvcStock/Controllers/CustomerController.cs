using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class CustomerController : Controller
    {
        MvcDbStockEntities db = new MvcDbStockEntities();
        // GET: Customer
        public ActionResult Index(string p)
        {
            var deger = from d in db.TMUSTERILERs select d;
            //var deger = db.TMUSTERILERs.ToList();
            if (!string.IsNullOrEmpty(p))
            {
                deger = deger.Where(m => m.MUSTERIAD.Contains(p) || m.MUSTERISOYAD.Contains(p));
            }
            
            return View(deger.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(TMUSTERILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            db.TMUSTERILERs.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var deletingCustomer = db.TMUSTERILERs.Find(id);
            db.TMUSTERILERs.Remove(deletingCustomer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringCustomer(int id)
        {
            var customer = db.TMUSTERILERs.Find(id);
            return View(customer);
        }
        public ActionResult Update(TMUSTERILER p1)
        {
            if(!ModelState.IsValid)
            {
                return View("BringCustomer");
            }
            var customer = db.TMUSTERILERs.Find(p1.MUSTERIID);
            customer.MUSTERIID = p1.MUSTERIID;
            customer.MUSTERIAD = p1.MUSTERIAD;
            customer.MUSTERISOYAD = p1.MUSTERISOYAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
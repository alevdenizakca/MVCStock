using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(int page=1)
        {
            //var degerler = db.TKATEGORILERs.ToList();
            var degerler = db.TKATEGORILERs.ToList().ToPagedList(page,4);
            return View(degerler);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TKATEGORILER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            db.TKATEGORILERs.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            var deletingCategory = db.TKATEGORILERs.Find(id);
            db.TKATEGORILERs.Remove(deletingCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult BringCategory(int id) 
        {
            var category = db.TKATEGORILERs.Find(id);
            return View("BringCategory", category);
        }
        
        public ActionResult Update(TKATEGORILER p1) 
        {
            var category = db.TKATEGORILERs.Find(p1.KATEGORIID);
            category.KATEGORIAD=p1.KATEGORIAD;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
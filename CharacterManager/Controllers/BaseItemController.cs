using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CharacterManager.DAL;
using CharacterManager.Models;

namespace CharacterManager.Controllers
{
    public class BaseItemController : Controller
    {
        private Context db = new Context();

        // GET: BaseItem
        public ActionResult Index()
        {
            return View(db.BaseItems.ToList());
        }

        // GET: BaseItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // GET: BaseItem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BaseItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BaseItemID,BaseItemName,BaseItemType,BaseItemValue,BaseItemWeight")] BaseItem baseItem)
        {
            if (ModelState.IsValid)
            {
                db.BaseItems.Add(baseItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(baseItem);
        }

        // GET: BaseItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // POST: BaseItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BaseItemID,BaseItemName,BaseItemType,BaseItemValue,BaseItemWeight")] BaseItem baseItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(baseItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(baseItem);
        }

        // GET: BaseItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BaseItem baseItem = db.BaseItems.Find(id);
            if (baseItem == null)
            {
                return HttpNotFound();
            }
            return View(baseItem);
        }

        // POST: BaseItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BaseItem baseItem = db.BaseItems.Find(id);
            db.BaseItems.Remove(baseItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

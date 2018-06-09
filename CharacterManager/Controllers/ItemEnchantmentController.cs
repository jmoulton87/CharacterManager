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
    public class ItemEnchantmentController : Controller
    {
        private Context db = new Context();

        // GET: ItemEnchantment
        public ActionResult Index()
        {
            var itemEnchanments = db.ItemEnchanments.Include(i => i.Enchantment).Include(i => i.Item);
            return View(itemEnchanments.ToList());
        }

        // GET: ItemEnchantment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEnchantment itemEnchantment = db.ItemEnchanments.Find(id);
            if (itemEnchantment == null)
            {
                return HttpNotFound();
            }
            return View(itemEnchantment);
        }

        // GET: ItemEnchantment/Create
        public ActionResult Create()
        {
            ViewBag.EnchantmentID = new SelectList(db.Enchantments, "EnchantmentID", "EnchantmentName");
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName");
            return View();
        }

        // POST: ItemEnchantment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemEnchantmentID,ItemID,EnchantmentID")] ItemEnchantment itemEnchantment)
        {
            if (ModelState.IsValid)
            {
                db.ItemEnchanments.Add(itemEnchantment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EnchantmentID = new SelectList(db.Enchantments, "EnchantmentID", "EnchantmentName", itemEnchantment.EnchantmentID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", itemEnchantment.ItemID);
            return View(itemEnchantment);
        }

        // GET: ItemEnchantment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEnchantment itemEnchantment = db.ItemEnchanments.Find(id);
            if (itemEnchantment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnchantmentID = new SelectList(db.Enchantments, "EnchantmentID", "EnchantmentName", itemEnchantment.EnchantmentID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", itemEnchantment.ItemID);
            return View(itemEnchantment);
        }

        // POST: ItemEnchantment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemEnchantmentID,ItemID,EnchantmentID")] ItemEnchantment itemEnchantment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itemEnchantment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EnchantmentID = new SelectList(db.Enchantments, "EnchantmentID", "EnchantmentName", itemEnchantment.EnchantmentID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "ItemName", itemEnchantment.ItemID);
            return View(itemEnchantment);
        }

        // GET: ItemEnchantment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItemEnchantment itemEnchantment = db.ItemEnchanments.Find(id);
            if (itemEnchantment == null)
            {
                return HttpNotFound();
            }
            return View(itemEnchantment);
        }

        // POST: ItemEnchantment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItemEnchantment itemEnchantment = db.ItemEnchanments.Find(id);
            db.ItemEnchanments.Remove(itemEnchantment);
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

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
    public class EnchantmentController : Controller
    {
        private Context db = new Context();

        // GET: Enchantment
        public ActionResult Index()
        {
            return View(db.Enchantments.ToList());
        }

        // GET: Enchantment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enchantment enchantment = db.Enchantments.Find(id);
            if (enchantment == null)
            {
                return HttpNotFound();
            }
            return View(enchantment);
        }

        // GET: Enchantment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Enchantment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnchantmentID,EnchantmentName,EnchantmentDesc,EnchantmentType,EnchantmentBonus,IsEnhancement,EnchantmentValue")] Enchantment enchantment)
        {
            if (ModelState.IsValid)
            {
                db.Enchantments.Add(enchantment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(enchantment);
        }

        // GET: Enchantment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enchantment enchantment = db.Enchantments.Find(id);
            if (enchantment == null)
            {
                return HttpNotFound();
            }
            return View(enchantment);
        }

        // POST: Enchantment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnchantmentID,EnchantmentName,EnchantmentDesc,EnchantmentType,EnchantmentBonus,IsEnhancement,EnchantmentValue")] Enchantment enchantment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enchantment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(enchantment);
        }

        // GET: Enchantment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enchantment enchantment = db.Enchantments.Find(id);
            if (enchantment == null)
            {
                return HttpNotFound();
            }
            return View(enchantment);
        }

        // POST: Enchantment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enchantment enchantment = db.Enchantments.Find(id);
            db.Enchantments.Remove(enchantment);
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

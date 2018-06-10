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
    public class IconController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Icon
        public ActionResult Index()
        {
            var icons = unitOfWork.IconRepository.Get();
            return View(icons.ToList());
        }

        // GET: Icon/Details/5
        public ActionResult Details(int id)
        {
            Icon icon = unitOfWork.IconRepository.GetByID(id);
            return View(icon);
        }

        // GET: Icon/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Icon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IconID,IconName,IconSheet,IconPosition")] Icon icon)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.IconRepository.Insert(icon);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(icon);
        }

        // GET: Icon/Edit/5
        public ActionResult Edit(int id)
        {
            Icon icon = unitOfWork.IconRepository.GetByID(id);
            return View(icon);
        }

        // POST: Icon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IconID,IconName,IconSheet,IconPosition")] Icon icon)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.IconRepository.Update(icon);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(icon);
        }

        // GET: Icon/Delete/5
        public ActionResult Delete(int id)
        {
            Icon icon = unitOfWork.IconRepository.GetByID(id);
            return View(icon);
        }

        // POST: Icon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Icon icon = unitOfWork.IconRepository.GetByID(id);
            unitOfWork.IconRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
                base.Dispose(disposing);
            }
            base.Dispose(disposing);
        }
    }
}





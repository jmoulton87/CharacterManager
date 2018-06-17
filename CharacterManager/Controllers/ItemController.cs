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
    public class ItemController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        public void MoveItem(int FirstItemID, int FirstLocationID, int FirstSlotID, int? SecondItemID, int SecondLocationID, int SecondSlotID)
        {
            if(SecondItemID != null)
            {
                unitOfWork.ItemRepository.GetByID(FirstItemID).Slot = SecondSlotID;
                unitOfWork.Save();
                unitOfWork.ItemRepository.GetByID(FirstItemID).LocationID = SecondLocationID;
                unitOfWork.Save();

                unitOfWork.ItemRepository.GetByID(SecondItemID).Slot = FirstSlotID;
                unitOfWork.Save();
                unitOfWork.ItemRepository.GetByID(SecondItemID).LocationID = FirstLocationID;
                unitOfWork.Save();
            }
            else
            {
                unitOfWork.ItemRepository.GetByID(FirstItemID).Slot = SecondSlotID;
                unitOfWork.Save();
                unitOfWork.ItemRepository.GetByID(FirstItemID).LocationID = SecondLocationID;
                unitOfWork.Save();
            }
        }

        public void EditQuantities(int FirstItemID, int FirstItemNewQuan, int SecondItemID, int SecondItemNewQuan)
        {

            if (FirstItemNewQuan == 0)
            {
                unitOfWork.ItemRepository.Delete(FirstItemID);
                unitOfWork.Save();
                unitOfWork.ItemRepository.GetByID(SecondItemID).Quantity = SecondItemNewQuan; 
                unitOfWork.Save();
            }
            else
            {
                unitOfWork.ItemRepository.GetByID(FirstItemID).Quantity = FirstItemNewQuan;
                unitOfWork.Save();
                unitOfWork.ItemRepository.GetByID(SecondItemID).Quantity = SecondItemNewQuan;
                unitOfWork.Save();
            }
        }

        //public void TradeItem(int ItemID, int LocationID)
        //{
        //    var destination = unitOfWork.LocationRepository.GetByID(LocationID);
        //    var items = unitOfWork.ItemRepository.Get().Where(x => x.LocationID == destination.LocationID).OrderBy(x => x.Slot);
        //    List<int> SlotList = new List<int>();
        //    foreach (var item in items)
        //    {
        //        SlotList.Add(item.Slot);
        //    }
        //    int destinationSlot = 0;
        //    ///FIX THIS UNREACHABLE CODE
        //    for (int i = 1; i < SlotList.Count; i++)
        //    {
        //        //if there are no items in destination
        //        //or if an open slot is found
        //        if (SlotList.Count == 0 || i != SlotList[i])
        //        {
        //            destinationSlot = i;
        //            break;
        //        } else
        //        //grab the next open slot
        //        {
        //            destinationSlot = i + 1;
        //            break;
        //        }   
        //    }
        //    unitOfWork.ItemRepository.GetByID(ItemID).LocationID = LocationID;
        //    unitOfWork.Save();
        //    unitOfWork.ItemRepository.GetByID(ItemID).Slot = destinationSlot;
        //}

        // GET: Item
        public ActionResult Index()
        {
            var items = unitOfWork.ItemRepository.Get();
            return View(items.ToList());
        }

        // GET: Item/Details/5
        public ActionResult Details(int id)
        {
            Item item = unitOfWork.ItemRepository.GetByID(id);
            return View(item);
        }

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,LocationID,BaseItemID,ItemName,ItemValue,Slot")] Item item)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ItemRepository.Insert(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Item/Edit/5
        public ActionResult Edit(int id)
        {
            Item item = unitOfWork.ItemRepository.GetByID(id);
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,LocationID,BaseItemID,ItemName,ItemValue,Slot")] Item item)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.ItemRepository.Update(item);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Item/Delete/5
        public ActionResult Delete(int id)
        {
            Item item = unitOfWork.ItemRepository.GetByID(id);
            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = unitOfWork.ItemRepository.GetByID(id);
            unitOfWork.ItemRepository.Delete(id);
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
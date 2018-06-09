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
using Microsoft.AspNet.Identity;

namespace CharacterManager.Controllers
{
    public class CharacterController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        //MAIN CHARACTER PAGE
        public ActionResult CharacterUI(int id)
        {
            Character character = unitOfWork.CharacterRepository.GetByID(id);
            ViewBag.Characters = new SelectList(unitOfWork.CharacterRepository.Get().Where(x => x.CampaignID == character.CampaignID && x.CharacterID != character.CharacterID), "CharacterID", "CharacterName");
            return View(character);
        }

        // GET: Character
        public ActionResult Index()
        {
            var characters = unitOfWork.CharacterRepository.Get();
            return View(characters.ToList());
        }

        // GET: Character/Details/5
        public ActionResult Details(int id)
        {
            Character character = unitOfWork.CharacterRepository.GetByID(id);
            return View(character);
        }

        // GET: Character/Create
        public ActionResult Create()
        {
            ViewBag.CampaignID = new SelectList(unitOfWork.CampaignRepository.Get(), "CampaignID", "CampaignName");
            return View();
        }

        // POST: Character/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CharacterID,CharacterName,CampaignID,CarryStrength")] Character character, string campaignID)
        {
            character.UserID = User.Identity.GetUserId();
            character.CharacterType = (CharacterType)2;
            if (ModelState.IsValid)
            {
                unitOfWork.CharacterRepository.Insert(character);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            //when a new character is created
            //create a new inventory linked to character
            //create new base loactions linked to character
            unitOfWork.InventoryRepository.Insert(new Inventory { CharacterID = character.CharacterID, InventoryName = character.CharacterName + "'s Inventory" });
            unitOfWork.Save();
            unitOfWork.LocationRepository.Insert(new Location { InventoryID = character.Inventories.First().InventoryID, LocationType = (LocationType)1, LocationName = "Equipment"});
            unitOfWork.LocationRepository.Insert(new Location { InventoryID = character.Inventories.First().InventoryID, LocationType = (LocationType)2, LocationName = "Inventory" });
            unitOfWork.LocationRepository.Insert(new Location { InventoryID = character.Inventories.First().InventoryID, LocationType = (LocationType)3, LocationName = "Trade" });
            unitOfWork.Save();



            return View(character);
        }

        // GET: Character/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CampaignID = new SelectList(unitOfWork.CampaignRepository.Get(), "CampaignID", "CampaignName");
            Character character = unitOfWork.CharacterRepository.GetByID(id);
            return View(character);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CharacterID,CharacterName,CampaignID,CarryStrength")] Character character, string campaignID)
        {
            character.UserID = User.Identity.GetUserId();
            character.CharacterType = (CharacterType)2;

            if (ModelState.IsValid)
            {
                unitOfWork.CharacterRepository.Update(character);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(character);
        }

        // GET: Character/Delete/5
        public ActionResult Delete(int id)
        {
            Character character = unitOfWork.CharacterRepository.GetByID(id);
            return View(character);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Character character = unitOfWork.CharacterRepository.GetByID(id);
            unitOfWork.CharacterRepository.Delete(id);
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

        public int[] CarryingCapacity(int CarryStrength)
        {
            int[] score = new int[29] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29 };
            int[] light = new int[29] { 3, 6, 10, 13, 16, 20, 23, 26, 30, 33, 38, 43, 50, 58, 66, 76, 86, 100, 116, 133, 153, 173, 200, 233, 266, 306, 346, 400, 466 };
            int[] medium = new int[29] { 6, 13, 20, 26, 33, 40, 46, 53, 60, 66, 76, 86, 100, 116, 133, 153, 173, 200, 233, 266, 306, 346, 400, 466, 533, 613, 693, 800, 933 };
            int[] heavy = new int[29] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 115, 130, 150, 175, 200, 230, 260, 300, 350, 400, 460, 520, 600, 700, 800, 920, 1040, 1200, 1400 };

            int i = Array.IndexOf(score, CarryStrength);

            int[] CarryCaps = new int[3] { light[i], medium[i], heavy[i] };
            return CarryCaps;
        }

        public int CurrentLoad()
        {
            int CurrentLoad = 0;
            return CurrentLoad;
        }
    }
}




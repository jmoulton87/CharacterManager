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
    [Authorize]
    public class CampaignController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: Campaign
        public ActionResult Index()
        {
            //get a list of all campaigns that you have a character in
            List<Campaign> campaigns = new List<Campaign>();

            var yourCharacters = unitOfWork.CharacterRepository.Get().Where(x => x.UserID == User.Identity.GetUserId());
            foreach(var character in yourCharacters)
            {
                campaigns.Add(character.Campaign);
            }

            return View(campaigns.ToList());
        }

        // GET: Campaign/Details/5
        public ActionResult Details(int id)
        {
            Campaign campaign = unitOfWork.CampaignRepository.GetByID(id);
            return View(campaign);
        }

        // GET: Campaign/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Campaign/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CampaignID,CampaignName")] Campaign campaign)
        {
            campaign.UserID = User.Identity.GetUserId();
            campaign.JoinLink = LinkID();

            if (ModelState.IsValid)
            {
                unitOfWork.CampaignRepository.Insert(campaign);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }

            unitOfWork.CharacterRepository.Insert(new Character { CampaignID = campaign.CampaignID, UserID = User.Identity.GetUserId(), CharacterName = "GM", CharacterType = (CharacterType)1, CarryStrength = 1 });
            unitOfWork.Save();

            return View(campaign);
        }

        // GET: Campaign/Edit/5
        public ActionResult Edit(int id)
        {
            Campaign campaign = unitOfWork.CampaignRepository.GetByID(id);
            return View(campaign);
        }

        // POST: Campaign/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CampaignID,CampaignName")] Campaign campaign)
        {

            campaign.UserID = User.Identity.GetUserId();

            if (ModelState.IsValid)
            {
                unitOfWork.CampaignRepository.Update(campaign);
                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(campaign);
        }

        // GET: Campaign/Delete/5
        public ActionResult Delete(int id)
        {
            Campaign campaign = unitOfWork.CampaignRepository.GetByID(id);
            return View(campaign);
        }

        // POST: Campaign/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Campaign campaign = unitOfWork.CampaignRepository.GetByID(id);
            unitOfWork.CampaignRepository.Delete(id);
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

        public string LinkID()
        {
            return Guid.NewGuid().ToString().GetHashCode().ToString("x");
        }
    }
}







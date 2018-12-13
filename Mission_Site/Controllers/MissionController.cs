﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mission_Site.DAL;
using Mission_Site.Models;

namespace Mission_Site.Controllers
{
    public class MissionController : Controller
    {
        private MissionContext db = new MissionContext();

        // GET: Mission
        public ActionResult Index()
        {
            return View(db.Mission.ToList());
        }

        // GET: Mission/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Mission.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        [Authorize]
        public ActionResult MissionFAQ(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Mission.Find(id);

            if (mission == null)
            {
                return HttpNotFound();
            }


            mission.MissionQuestions = db.Database.SqlQuery<MissionQuestions>("SELECT * FROM dbo.MissionQuestions WHERE MissionID = " + id);

            return View(mission);
        }

        // GET: Mission/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mission/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "missionID,missionName,presidentName,missionAddress,missionLanguage,missionClimate,missionDominateReligion")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Mission.Add(mission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mission);
        }

        // GET: Mission/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Mission.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Mission/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "missionID,missionName,presidentName,missionAddress,missionLanguage,missionClimate,missionDominateReligion")] Mission mission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mission);
        }

        // GET: Mission/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mission mission = db.Mission.Find(id);
            if (mission == null)
            {
                return HttpNotFound();
            }
            return View(mission);
        }

        // POST: Mission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mission mission = db.Mission.Find(id);
            db.Mission.Remove(mission);
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

        public ActionResult MissionSelect()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            string email = form["Email"].ToString();
            string password = form["Password"].ToString();

            //Users returnedRecord = new Users();

            var returnedRecord = db.Database.SqlQuery<int>("SELECT UserID FROM Users WHERE UserEmail = " + email + " AND password = " + password);

            if (returnedRecord > 0)
            {
                FormsAuthentication.SetAuthCookie(email, rememberMe);

                return RedirectToAction("MissionSelect", "Mission");
            }
            else
            {
                return View();
            }
        }

        public ActionResult newAccount()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult newAccount([Bind(Include = "userID,userEmail,password,firstName,lastName")] Users users, bool rememberMe = false)
        {
            string email = users.userEmail;
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(email, rememberMe);
                return RedirectToAction("Index", "Home");
            }

            return View(users);
        }
    }
}

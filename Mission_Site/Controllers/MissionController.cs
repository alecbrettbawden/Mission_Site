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
            List<Mission> missionList = db.Mission.ToList();
            return View(missionList);
        }

        public ActionResult UpdateAnswer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MissionQuestions missionQuestions = db.MissionQuestions.Find(id);
            if (missionQuestions == null)
            {
                return HttpNotFound();
            }
            return View(missionQuestions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAnswer([Bind(Include = "missionquestionID,missionID,userID,question,answer")] MissionQuestions missionQuestions)
        {
            if (ModelState.IsValid)
            {
                missionQuestions.question = missionQuestions.question;
                db.Entry(missionQuestions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MissionFAQ", new { id = missionQuestions.missionID});
            }
            return View(missionQuestions);
        }

        // GET: MissionQuestions/CreateQuestion
        public ActionResult AskQuestion(int id)
        {
            ViewBag.missionID = id;
            Mission mission = db.Mission.Find(id);
            ViewBag.missionName = mission.missionName;

            return View();
        }

        // POST: Mission/askQuestion
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AskQuestion([Bind(Include = "missionquestionID,missionID,userID,question,answer,missionDominateReligion")] MissionQuestions missionQuestions, int id)
        {
            if (ModelState.IsValid)
            {
                             List<Users> returnedRecord = db.Users.ToList();
             
                foreach (var record in returnedRecord)
                {
                    if (record.userEmail == User.Identity.Name)
                    {
                        missionQuestions.userID = record.userID;
                    }
                }

                missionQuestions.missionID = id;
                db.MissionQuestions.Add(missionQuestions);
                db.SaveChanges();
                return RedirectToAction("MissionFAQ","Mission", new { id });
            }

            return View(missionQuestions);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            string email = form["Email"].ToString();
            string password = form["Password"].ToString();

            List<Users> returnedRecord = db.Users.ToList();
            int count = 0;
            foreach (var record in returnedRecord)
            {
                if (record.userEmail == email && record.password == password)
                {
                    count++;
                }
            }

            if (count > 0)
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
        public ActionResult newAccount([Bind(Include = "userID,userEmail,password,first_name,last_name")] Users users, bool rememberMe = false)
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

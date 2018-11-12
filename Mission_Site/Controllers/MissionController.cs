using Mission_Site.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mission_Site.Controllers
{
    public class MissionController : Controller
    {

        public static List<Mission> missionList = new List<Mission>();


        public ActionResult Index()
        {
            return View();
        }

        /*[HttpPost]
        public ActionResult NewMission(Mission newMission)
        {
            missionList.Add(newMission);
            return View();
        }*/

        public ActionResult MissionFAQ(int missionID)
        {
            return View(missionList);
        }
    }
}
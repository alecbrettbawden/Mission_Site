using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mission_Site.Models
{
    public class Mission
    {
        public int missionID { get; set; }
        public string missionName { get; set; }
        public string missionPresidentName { get; set; }
        public string missionStreetAddress { get; set; }
        public string missionCity { get; set; }
        public int missionZip { get; set; }
        public string? missionState { get; set; }
        public string missionCountry { get; set; }
    }
}
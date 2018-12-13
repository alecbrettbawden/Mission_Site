using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mission_Site.Models
{
    [Table("Missions")]
    public class Mission
    {
        [Key]
        public int missionID { get; set; }

        [DisplayName("Name")]
        public string missionName { get; set; }

        [DisplayName("Mission President")]
        public string presidentName { get; set; }

        [DisplayName("Address")]
        public string missionAddress { get; set; }

        [DisplayName("Language")]
        public string missionLanguage { get; set; }

        [DisplayName("Climate")]
        public string missionClimate { get; set; }

        [DisplayName("Dominate Religion")]
        public string missionDominateReligion { get; set; }

        public string missionImage { get; set; }

        public IEnumerable<MissionQuestions> MissionQuestions {get; set;}
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mission_Site.Models
{
    [Table("MissionQuestions")]
    public class MissionQuestions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int missionquestionID { get; set; }

        public int missionID { get; set; }
        public virtual Mission mission { get; set; }


        public int userID { get; set; }
        [DisplayName("Question:")]
        public string question { get; set; }
        [DisplayName("Answer:")]
        public string answer { get; set; }
        

       
    }
}
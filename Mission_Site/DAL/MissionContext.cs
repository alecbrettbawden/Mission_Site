using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Mission_Site.Models;

namespace Mission_Site.DAL
{
    public class MissionContext : DbContext
    {
        public MissionContext() : base("MissionContext")
        {

        }

        public DbSet<Mission> Mission { get; set; }

        public System.Data.Entity.DbSet<Mission_Site.Models.MissionQuestions> MissionQuestions { get; set; }

        public System.Data.Entity.DbSet<Mission_Site.Models.Users> Users { get; set; }
    }
}
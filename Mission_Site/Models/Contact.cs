using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Mission_Site.Models
{
    public class Contact
    {
        [DisplayName("User Name")]
        public string userName { get; set; }

        [DisplayName("Email")]
        [EmailAddress(ErrorMessage ="That is not a valid email address")]
        public string userEmail { get; set; }

        [DisplayName("Subject")]
        public string subject { get; set; }

        [DisplayName("Message Body")]
        public string messageBody { get; set; }

    }
}
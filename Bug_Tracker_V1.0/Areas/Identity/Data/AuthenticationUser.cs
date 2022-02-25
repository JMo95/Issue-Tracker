using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Microsoft.AspNetCore.Identity;

namespace Bug_Tracker_V1._0.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AuthenticationUser class
    public class AuthenticationUser : IdentityUser<int>
    {

        // To tell the db it is personal data we add this
        // for column attribute we add this
        // This adds it to the asp.net usernames table


        [Column(TypeName = "varchar(100)")]
        public string FirstName { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        public byte[] ProfilePicture { get; set; }

        // Add list of projects
        public virtual ICollection<UserProject> UserProjects { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

    }
}

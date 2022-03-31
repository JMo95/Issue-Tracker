using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // for primary keys
using System.ComponentModel;

namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class Project
    {
        //Changed Display Names
        [DisplayName("Project Id")]
        public int ProjectId { get; set; }
        [DisplayName("Project Name")]
        public string ProjectName { get; set; }
        [DisplayName("Project Description")]
        public string ProjectDescription { get; set; }
        [DisplayName("Project Team Size")]
        public int ProjectTeamSize { get; set; }
        [DisplayName("Created By")]
        public string ProjectCreatedBy { get; set; }
        [DisplayName("Created At")]
        public DateTime ProjectCreatedTime { get; set; } = DateTime.Now;
        public int ProjectOwnerId { get; set; }

        public virtual ICollection<UserProject> UserProjects { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}

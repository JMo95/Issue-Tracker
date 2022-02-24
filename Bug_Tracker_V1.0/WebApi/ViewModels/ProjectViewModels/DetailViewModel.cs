using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using Bug_Tracker_V1._0.Models.Enums;
using Bug_Tracker_V1._0.ViewModels.AppUserViewModels;
using Bug_Tracker_V1._0.ViewModels.TicketViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.ViewModels.ProjectViewModels
{
    public class DetailViewModel
    {
        public Project Project { get; set; }
        public AuthenticationUser User { get; set; }
        public TicketCreateViewModel TicketCreateVm { get; set; }
        [DisplayName("Role")]
        public Role UserProjectRole { get; set; }
        [DisplayName("Status")]
        public Status TicketStatus { get; set; }
        public string NewComment { get; set; }
        [DisplayName("Assigned To")]
        public string AssignedTo { get; set; }
        [DisplayName("Open Tickets")]
        public int OpenTicketCount { get; set; }
        [DisplayName("Closed Tickets")]
        public int ClosedTicketCount { get; set; }
        [DisplayName("Tickets In Progress")]
        public int InProgressTicketCount { get; set; }
        public IEnumerable<AuthenticationUser> Collaborators { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}

using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using Bug_Tracker_V1._0.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.ViewModels.AppUserViewModels
{
    public class CollaborateViewModel
    {
        public IEnumerable<AuthenticationUser> Collaborators { get; set; }
        [DisplayName("Non Collaborators")]
        public IEnumerable<SelectListItem> NonCollaborators { get; set; }
       // public IEnumerable<Notification> Notifications { get; set; }
     //   public List<string> SelectedCollaborators { get; set; }
        public List<String> SelectedCollaborators { get; set; }
        public int ProjectId { get; set; }
        [DisplayName("Role")]
        public Role UserRole { get; set; }
    } 
}

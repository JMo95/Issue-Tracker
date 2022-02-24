  using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using Bug_Tracker_V1._0.Models.Enums;
using Bug_Tracker_V1._0.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.ViewModels.TicketViewModels
{
    public class TicketCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public TicketType Type { get; set; }

        public IEnumerable<SelectListItem> PriorityOptions { get; set; }
        public IEnumerable<SelectListItem> StatusOptions { get; set; }
        public IEnumerable<SelectListItem> TypeOptions { get; set; }
        public IEnumerable<SelectListItem> ProjectUsers { get; set; }

        public int AssignedToId { get; set; }

        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }

        public int TicketCountByStatus { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Comment Comment { get; set; }
        public Ticket Ticket { get; set; }
    
        public int TicketId { get; set; }
        public AuthenticationUser User { get; set; }
        public Role UserProjectRole { get; set; }
        public ICollection<TicketHistory> TicketHistory { get; set; }
        public string userFullName { get; set; }
        public virtual List<Attachments> Attachments { get; set; }
        public List<IFormFile> files { get; set; }
        public string fileDescription { get; set; }
        public IEnumerable<Ticket> UserTicketList { get; set; }
        public DetailViewModel Detailvm { get; set; }
        public int OpenTicketCount { get; set; }
        public int ClosedTicketCount { get; set; }
        public string AssignedTo { get; set; }
    }
}

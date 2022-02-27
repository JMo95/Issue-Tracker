using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // for primary keys
using System.ComponentModel;
using Bug_Tracker_V1._0.Models.Enums;

namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Priority Priority { get; set; } = Priority.Medium;

        public Status Status { get; set; } = Status.Open;
        public TicketType Type { get; set; } = TicketType.Task;

        public int RequestorId { get; set; }
        public int AssignedToId { get; set; }
        public string AssignedTo { get; set;  }
        public int ProjectId { get; set; }
        public virtual ICollection<Comment> Comments {get; set;}
        public int CommentCount { get; set; }
        public virtual ICollection<TicketHistory> TicketHistory { get; set; }
        public virtual List<Attachments> Attachments { get; set; }
    }
}

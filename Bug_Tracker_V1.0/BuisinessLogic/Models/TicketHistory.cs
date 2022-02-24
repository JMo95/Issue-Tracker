using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class TicketHistory
    {
        [Key]
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ProjectId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Property { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}

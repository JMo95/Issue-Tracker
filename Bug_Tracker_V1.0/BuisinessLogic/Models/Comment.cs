using Bug_Tracker_V1._0.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int CommenterId { get; set; }
        public int CommenterName { get; set; }
        public string FullName { get; set; }
        public string CommentText { get; set; }
        public AuthenticationUser User { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}

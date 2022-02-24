using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class UserProject
    {
        public int AuthenticationUserId { get; set; }
        public AuthenticationUser AuthenticationUser { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
        public Role ProjectRole { get; set; }
    }
}

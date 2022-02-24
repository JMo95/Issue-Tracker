using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Bug_Tracker_V1._0.BuisinessLogic.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int AuthenticationUserId { get; set; }
        public int ProjectId { get; set; }
        public int ProjectOwnerId { get; set; }
        public string NofiticationMessage { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool isAcknowledged { get; set; } = false;
        public Role UserProjectRole { get; set; }

    }
}

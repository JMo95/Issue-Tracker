using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.WebApi.ViewModels.AppUserViewModels
{
    public class NotificationsViewModel
    {
        
        public IEnumerable<Notification> UserNotifications { get; set; }
        public int AuthenticationUserId { get; set; }
        public int NotificationCount { get; set; }
    }
}

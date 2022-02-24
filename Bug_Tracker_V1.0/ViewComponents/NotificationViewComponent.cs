using Bug_Tracker_V1._0.Facades;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.WebApi.ViewModels.AppUserViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.ViewComponents
{
    public class NotificationViewComponent : ViewComponent
    {
        private readonly IProjectFacade _projectFacade;
        public NotificationViewComponent(IProjectFacade projectFacade)
        {
            _projectFacade = projectFacade;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var uid = await _projectFacade.GetUser(HttpContext.User);

            var vm = new NotificationsViewModel
            {
                AuthenticationUserId = uid.Id,
                UserNotifications = await _projectFacade.getAllNotificationsByUserId(uid.Id)
            };
            return View(vm);
        }
    }
}

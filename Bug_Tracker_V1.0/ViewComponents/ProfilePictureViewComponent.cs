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
    public class ProfilePictureViewComponent : ViewComponent
    {
        private readonly IAppUserService _appUserService;
        private readonly IProjectFacade _projectFacade;

        public ProfilePictureViewComponent(IAppUserService appUserService, IProjectFacade projectFacade)
        {
            _appUserService = appUserService;
            _projectFacade = projectFacade;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _projectFacade.GetUser(HttpContext.User);

            var vm = new AppUserViewModel
            {
                User = user,
                ProfilePicture = user.ProfilePicture
            };
            return View(vm);

        }
    }
}

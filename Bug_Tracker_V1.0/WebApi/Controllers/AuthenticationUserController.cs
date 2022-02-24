using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Controllers
{
    [Authorize]
    public class AuthenticationUserController : Controller
    {
        private readonly IAppUserService _userService;


        public AuthenticationUserController(IAppUserService appUserService)
            
        {
            _userService = appUserService;
        }
    }
}

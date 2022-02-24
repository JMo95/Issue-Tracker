using Bug_Tracker_V1._0.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.WebApi.ViewModels.AppUserViewModels
{
    public class AppUserViewModel
    {
        [DisplayName("Profile Picture")]
        public byte[] ProfilePicture { get; set; }
        public AuthenticationUser User { get; set; }
    }
}

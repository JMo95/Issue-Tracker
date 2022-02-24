using Bug_Tracker_V1._0.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface IAppUserService
    {
        Task<AuthenticationUser> FindOne(int id);
        Task<IEnumerable<AuthenticationUser>> GetAll();
        Task<AuthenticationUser> GetUserByClaim(ClaimsPrincipal principal);
    }
}

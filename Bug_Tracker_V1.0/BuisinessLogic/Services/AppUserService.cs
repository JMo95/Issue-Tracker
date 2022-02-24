using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepo;

        public AppUserService(IAppUserRepository appUserRepo)
        {
            _appUserRepo = appUserRepo;
        }

        public async Task<AuthenticationUser> FindOne(int id)
        {
            return await _appUserRepo.FindOne(id);
        }

        public async Task<IEnumerable<AuthenticationUser>> GetAll()
        {
            return await _appUserRepo.GetAll();
        }

        public async Task<AuthenticationUser> GetUserByClaim(ClaimsPrincipal principal)
        {
            var userId = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _appUserRepo.FindOne(userId);
        }
    }
}

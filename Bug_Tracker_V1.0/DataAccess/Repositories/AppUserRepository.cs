using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.Data;
using Bug_Tracker_V1._0.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;

        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AuthenticationUser> FindOne(int id)
        {
            return await _context.AuthenticationUsers
                                          .Include(x => x.UserProjects)
                                          .ThenInclude(x => x.Project)
                                          .ThenInclude(x => x.Tickets)
                                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AuthenticationUser>> GetAll()
        {
            return await _context.AuthenticationUsers.ToListAsync();
        }
    }
}

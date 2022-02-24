using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Data;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Models;
using Bug_Tracker_V1._0.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Project entity)
        {
            await _context.Projects.AddAsync(entity);

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            _context.Projects.Remove(project);
            await SaveChanges();

            return 1;
        }

        public async Task<Project> FindOne(int id)
        {
            return await _context.Projects.Include(x => x.Tickets)
                                          .Include(x => x.UserProjects)
                                          .ThenInclude(x => x.AuthenticationUser)
                                          .FirstOrDefaultAsync(x => x.ProjectId == id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Project entity)
        {
            var project = await _context.Projects.FindAsync(entity.ProjectId);
            _context.Entry(project).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}

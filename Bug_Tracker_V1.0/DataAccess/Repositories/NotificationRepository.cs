using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.DataAccess.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Notification entity)
        {
            await _context.Notifications.AddAsync(entity);
            await SaveChanges();
            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(notification);
            await SaveChanges();
            return 1;
        }
        
        public async Task<Notification> FindOne(int id)
        {
            return await _context.Notifications.FindAsync(id);
        }

        // Comparing this with the ticket repository, I think I should be including the AuthenticationUser when return the list.
        // Need to decide if there's anything else I should be returning.
        public async Task<IEnumerable<Notification>> GetAll()
        {
            return await _context.Notifications.ToListAsync();
        }

        public async Task<int> Update(Notification entity)
        {
            var Notification = await _context.Notifications.FindAsync(entity.Id);
            _context.Entry(Notification).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;

        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}

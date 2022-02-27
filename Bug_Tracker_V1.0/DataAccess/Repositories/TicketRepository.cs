using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Data;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Infrastructure.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Ticket entity)
        {
            await _context.Tickets.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await SaveChanges();

            return 1;
        }

        public async Task<Ticket> FindOne(int id)
        {
            return await _context.Tickets.Include(x => x.Comments)
                                         .Include(x => x.TicketHistory)
                                         .Include(x => x.Attachments)
                                         .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _context.Tickets.Include( x=> x.Comments)
                                         .ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Ticket entity)
        {
            var ticket = await _context.Tickets.FindAsync(entity.Id);
            _context.Entry(ticket).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}

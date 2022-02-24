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
        public class TicketHistoryRepository : ITicketHistoryRepository
        {
            private readonly ApplicationDbContext _context;

            public TicketHistoryRepository(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<int> Create(TicketHistory entity)
            {
                await _context.TicketHistory.AddAsync(entity);
                await SaveChanges();

                return 1;
            }

            public async Task<int> Delete(int id)
            {
                var ticketHistory = await _context.TicketHistory.FindAsync(id);
                _context.TicketHistory.Remove(ticketHistory);
                await SaveChanges();

                return 1;
            }

            public async Task<TicketHistory> FindOne(int id)
            {
                return await _context.TicketHistory.FirstOrDefaultAsync(x => x.Id == id);
            }

            public async Task<IEnumerable<TicketHistory>> GetAll()
            {
                return await _context.TicketHistory.ToListAsync();
            }

            public async Task SaveChanges()
            {
                await _context.SaveChangesAsync();
            }

            public async Task<int> Update(TicketHistory entity)
            {
                var ticketHistory = await _context.TicketHistory.FindAsync(entity.Id);
                _context.Entry(ticketHistory).CurrentValues.SetValues(entity);
                await SaveChanges();

                return 1;
            }
        }
}

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
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AttachmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Attachments entity)
        {
            await _context.Attachments.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            _context.Attachments.Remove(attachment);
            await SaveChanges();

            return 1;
        }

        public async Task<Attachments> FindOne(int id)
        {
            return await _context.Attachments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Attachments>> GetAll()
        {
            return await _context.Attachments.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Attachments entity)
        {
            var attachment = await _context.Attachments.FindAsync(entity.Id);
            _context.Entry(attachment).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }
    }
}

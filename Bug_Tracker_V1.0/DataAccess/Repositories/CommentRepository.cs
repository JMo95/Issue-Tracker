using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.BusinessLogic.Interfaces;
using Bug_Tracker_V1._0.Data;
using Bug_Tracker_V1._0.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Create(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
            await SaveChanges();

            return 1;
        }

        public async Task<int> Delete(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await SaveChanges();

            return 1;
        }

        public async Task<Comment> FindOne(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> Update(Comment entity)
        {
            var ticket = await _context.Comments.FindAsync(entity.Id);
            _context.Entry(ticket).CurrentValues.SetValues(entity);
            await SaveChanges();

            return 1;
        }

    }
}


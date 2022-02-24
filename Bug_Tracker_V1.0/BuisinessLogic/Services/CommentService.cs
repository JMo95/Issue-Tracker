using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.BusinessLogic.Interfaces;
using Bug_Tracker_V1._0.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BusinessLogic.Services
{
    public class CommentService : ICommentService
    {
        ICommentRepository _commentRepo;

        public CommentService(ICommentRepository commentRepo)
        {
            _commentRepo = commentRepo;
        }

        public async Task<int> Create(Comment entity)
        {
            return await _commentRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _commentRepo.Delete(id);
        }

        public async Task<Comment> FindOne(int id)
        {
            return await _commentRepo.FindOne(id);
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            return await _commentRepo.GetAll();
        }

        public async Task<int> Update(Comment entity)
        {
            return await _commentRepo.Update(entity);
        }

    }
}


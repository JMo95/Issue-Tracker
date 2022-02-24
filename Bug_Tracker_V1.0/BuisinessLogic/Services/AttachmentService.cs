using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BuisinessLogic.Services
{
    public class AttachmentService : IAttachmentService
    {
        IAttachmentRepository _attachmentRepo;

        public AttachmentService(IAttachmentRepository attachmentRepo)
        {
            _attachmentRepo = attachmentRepo;
        }

        public async Task<int> Create(Attachments entity)
        {
            return await _attachmentRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _attachmentRepo.Delete(id);
        }

        public async Task<Attachments> FindOne(int id)
        {
            return await _attachmentRepo.FindOne(id);
        }

        public async Task<IEnumerable<Attachments>> GetAll()
        {
            return await _attachmentRepo.GetAll();
        }

        public async Task<int> Update(Attachments entity)
        {
            return await _attachmentRepo.Update(entity);
        }
    }
}

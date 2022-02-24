using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.BuisinessLogic.Services
{
    public class TicketHistoryService : ITicketHistoryService
    {
        private readonly ITicketHistoryRepository _ticketRepo;

        public TicketHistoryService(ITicketHistoryRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }
        public async Task<int> Create(TicketHistory entity)
        {
            return await _ticketRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _ticketRepo.Delete(id);
        }

        public async Task<TicketHistory> FindOne(int id)
        {
            return await _ticketRepo.FindOne(id);
        }

        public async Task<IEnumerable<TicketHistory>> GetAll()
        {
            return await _ticketRepo.GetAll();
        }

        public async Task<int> Update(TicketHistory entity)
        {
            return await _ticketRepo.Update(entity);
        }

    }
}

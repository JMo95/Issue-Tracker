﻿using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepo;

        public TicketService(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        public async Task<int> Create(Ticket entity)
        {
            return await _ticketRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _ticketRepo.Delete(id);
        }

        public async Task<Ticket> FindOne(int id)
        {
            return await _ticketRepo.FindOne(id);
        }

        public async Task<IEnumerable<Ticket>> GetAll()
        {
            return await _ticketRepo.GetAll();
        }

        public async Task<IEnumerable<Ticket>> GetAllTicketsByUserId(int id)
        {
            var tickets = await _ticketRepo.GetAll();

            return tickets.Where(x => x.RequestorId == id || x.AssignedToId == id);
        }

        public async Task<int> Update(Ticket entity)
        {
            return await _ticketRepo.Update(entity);
        }

    }
}

using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface ITicketService : IGenericService<Ticket>
    {
        Task<IEnumerable<Ticket>> GetAllTicketsByUserId(int id);
    }
}

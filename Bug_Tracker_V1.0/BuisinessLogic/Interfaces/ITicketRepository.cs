using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Bug_Tracker_V1._0.Interfaces.IGenericRepository;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {

    }
}

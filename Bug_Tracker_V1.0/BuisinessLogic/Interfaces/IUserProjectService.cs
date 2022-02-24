using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface IUserProjectService : IGenericService<UserProject>
    {
        Task<IEnumerable<UserProject>> GetAllByUserId(int id);
        Task<IEnumerable<AuthenticationUser>> GetUserCollabsByProjectId(int id);
        Task<IEnumerable<AuthenticationUser>> GetNonCollabUsersByProjectId(int id);


    }
}

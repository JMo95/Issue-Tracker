using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Interfaces
{
    public interface IProjectService : IGenericService<Project>
    {
        Task<IEnumerable<SelectListItem>> GetUsersAsSelectListItemsByProjectId(int id);
    }
}

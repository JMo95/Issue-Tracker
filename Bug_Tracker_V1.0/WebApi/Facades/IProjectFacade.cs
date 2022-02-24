using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;
using Bug_Tracker_V1._0.Models.Enums;
using Bug_Tracker_V1._0.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Facades
{
    public interface IProjectFacade
    {
        Task<AuthenticationUser> GetUser(ClaimsPrincipal principal);

        Task<int> CreateUserProject(UserProject userProject);

        Task<IEnumerable<SelectListItem>> GetUserSelectListByProjectId(int id);
        int GetUserId(ClaimsPrincipal principal);
        Task<Project> FindProjectById(int id);

        Task<int> UpdateProject(Project project);
        Task<int> CreateProject(Project project);
        ReportsViewModel GetReportsViewModel(AuthenticationUser user, int projectId);

        Task<int> GetProjectTicketCountByStatus(int id, Status status);

        Task<IEnumerable<Notification>> getAllNotificationsByUserId(int id);

        Task<Boolean> UnaknowledgedNotificationExistsForUser(Notification entity);

        Task<IEnumerable<UserProject>> GetUserProjects(AuthenticationUser User);

        Role GetUserProjectRole(IEnumerable<UserProject> userProjects, int projectId);
        int GetTeamSize(IEnumerable<AuthenticationUser> collabUsers);
        Task<IEnumerable<Ticket>> GetUserTickets(AuthenticationUser User);
        Task<int> GetUserTicketCountByStatus(AuthenticationUser User, Status status);

    }
}

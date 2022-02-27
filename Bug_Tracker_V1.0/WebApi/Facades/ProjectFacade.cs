using AutoMapper;
using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Models.Enums;
using Bug_Tracker_V1._0.Services;
using Bug_Tracker_V1._0.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using System.Security.Principal;
using Bug_Tracker_V1._0.BuisinessLogic.Models.Enums;

namespace Bug_Tracker_V1._0.Facades
{
    public class ProjectFacade : IProjectFacade
    {
        private readonly IAppUserService _appUserService;
        private readonly IProjectService _projectService;
        private readonly IUserProjectService _userProjectService;
        private readonly ITicketService _ticketService;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public ProjectFacade(IAppUserService appUserService, IProjectService projectService,
            IUserProjectService userProjectService, IMapper mapper, ITicketService ticketService,
            INotificationService notificationService)
        {
            _appUserService = appUserService;
            _projectService = projectService;
            _userProjectService = userProjectService;
            _mapper = mapper;
            _ticketService = ticketService;
            _notificationService = notificationService;
        }

        public async Task<AuthenticationUser> GetUser(ClaimsPrincipal principal)
        {
            var userId = Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
            return await _appUserService.FindOne(userId);
        }

        public async Task<int> CreateUserProject(UserProject userProject)
        {
            return await _userProjectService.Create(userProject);
        }

        public async Task<IEnumerable<SelectListItem>> GetUserSelectListByProjectId(int id)
        {
            return await _projectService.GetUsersAsSelectListItemsByProjectId(id);
        }

        public async Task<Project> FindProjectById(int id)
        {
            return await _projectService.FindOne(id);
        }

        public async Task<int> UpdateProject(Project project)
        {
            return await _projectService.Update(project);
        }

        public async Task<int> CreateProject(Project project)
        {
            return await _projectService.Create(project);
        }

        public int GetUserId(ClaimsPrincipal principal)
        {
            return Convert.ToInt32(principal.FindFirstValue(ClaimTypes.NameIdentifier));
        }


        public ReportsViewModel GetReportsViewModel(AuthenticationUser user, int projectId)
        {
            var project = user.UserProjects.Select(x => x.Project).FirstOrDefault(x => x.ProjectId == projectId);

            return new ReportsViewModel
            {
                ProjectId = projectId,
                TotalTicketPriorityHigh = project.Tickets.Where(x => x.Priority == Priority.High).Count(),
                TotalTicketsPriorityLow = project.Tickets.Where(x => x.Priority == Priority.Low).Count(),
                TotalTicketsPriorityMed = project.Tickets.Where(x => x.Priority == Priority.Medium).Count(),
                TotalTicketsPriorityUrgent = project.Tickets.Where(x => x.Priority == Priority.Urgent).Count(),
                TotalTicketsStatusClosed = project.Tickets.Where(x => x.Status == Status.Closed).Count(),
                TotalTicketsStatusOpen = project.Tickets.Where(x => x.Status == Status.Open).Count(),
                TotalTicketsStatusInProgress = project.Tickets.Where(x => x.Status == Status.InProgress).Count(),
                TotalTicketsTypeTask = project.Tickets.Where(x => x.Type == TicketType.Task).Count(),
                TotalTicketsTypeBug = project.Tickets.Where(x => x.Type == TicketType.Bug).Count(),
                TotalTicketsTypeFeature = project.Tickets.Where(x => x.Type == TicketType.Feature).Count()
            };

        }

        public async Task<int> GetProjectTicketCountByStatus(int id, Status status)
        {
            var project = await _projectService.FindOne(id);
            int count = 0;

            foreach (var ticket in project.Tickets)
            {
                if (ticket.Status.Equals(status))
                {
                    count++;
                }
            }
            return count;
        }
        public async Task<int> GetUserTicketCountByStatus(AuthenticationUser User, Status status)
        {
            IEnumerable<Ticket> ticketList = await _ticketService.GetAll();
            IEnumerable<Ticket> userTicketList = ticketList.Where(x => x.AssignedToId == User.Id || x.RequestorId == User.Id);
            int count = 0;

            foreach (var ticket in userTicketList)
            {
                if (ticket.Status.Equals(status))
                {
                    count++;
                }
            }
            return count;
        }

        public async Task<IEnumerable<Notification>> getAllNotificationsByUserId(int id)
        {
            //error occuring here. 
            IEnumerable<Notification> notifications = await _notificationService.GetAll();
            IEnumerable<Notification> userNotifications = notifications.Where(x => x.AuthenticationUserId == id);
            return userNotifications;
        }

        public async Task<Boolean> UnaknowledgedNotificationExistsForUser(Notification entity)
        {
            IEnumerable<Notification> userNotifications = await getAllNotificationsByUserId(entity.AuthenticationUserId);

            // Check if user has a notification that is not aknowledged with given project id
            if (userNotifications.Any(x => x.ProjectId == entity.ProjectId && x.isAcknowledged == false)) 
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<IEnumerable<UserProject>> GetUserProjects(AuthenticationUser User)
        {
            // Get list of all user projects
            IEnumerable<UserProject> projectList = await _userProjectService.GetAll();
            // Get users list of projects
            IEnumerable<UserProject> userProjectList = projectList.Where(x => x.AuthenticationUserId == User.Id);

            return userProjectList;
        }

        public async Task<IEnumerable<Ticket>> GetUserTickets(AuthenticationUser User)
        {
            IEnumerable<Ticket> ticketList = await _ticketService.GetAll();
            IEnumerable<Ticket> userTicketList = ticketList.Where(x => x.AssignedToId == User.Id || x.RequestorId == User.Id);

            return userTicketList;
        }


        public Role GetUserProjectRole(IEnumerable<UserProject> userProjects, int projectId)
        {
            Role UserRole = userProjects.Where(x => x.ProjectId == projectId)
                                        .Select(x => x.ProjectRole)
                                        .FirstOrDefault();
            return UserRole;                               
        }

        public int GetTeamSize(IEnumerable<AuthenticationUser> collabUsers)
        {
            int count = 0;
            foreach (var user in collabUsers)
            {
                count++;
            }
            return count;
        }

        public async Task RemoveProjectFKs(int id)
        {
            var project = await _projectService.FindOne(id);

            foreach(var ticket in project.Tickets)
            {
                await RemoveTicketFKs(ticket.Id);
                project.Tickets.Remove(ticket);
            }

            foreach (var userProject in project.UserProjects)
            {
                project.UserProjects.Remove(userProject);
            }

            await _projectService.Update(project);
        }

        public async Task RemoveTicketFKs(int id)
        {
            var ticket = await _ticketService.FindOne(id);

            foreach (var attachment in ticket.Attachments)
            {
                ticket.Attachments.Remove(attachment);
            }

            foreach (var attachment in ticket.Attachments)
            {
                ticket.Attachments.Remove(attachment);
            }
            foreach (var comment in ticket.Comments)
            {
                ticket.Comments.Remove(comment);
            }
            foreach (var ticketHistory in ticket.TicketHistory)
            {
                ticket.TicketHistory.Remove(ticketHistory);
            }

            await _ticketService.Update(ticket);
        }

    }
}

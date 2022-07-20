
using AutoMapper;
using Bug_Tracker_V1._0.WebApi.Profiles;
using Bug_Tracker_V1._0.Facades;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.ViewModels.ProjectViewModels;
using Bug_Tracker_V1._0.ViewModels.TicketViewModels;
using Bug_Tracker_V1._0.ViewModels.AppUserViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug_Tracker_V1._0.Models.Enums;
using Bug_Tracker_V1._0.WebApi.Controllers;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.Services;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace Bug_Tracker_V1._0.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly ITicketHistoryService _ticketHistoryService;
        private readonly IAppUserService _appUserService;
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;
        private readonly IProjectFacade _projectFacade;
        private readonly IUserProjectService _userProjectService;

        public TicketController(ITicketService ticketService, ITicketHistoryService ticketHistoryService, IAppUserService appUserService, IMapper mapper,
            IProjectFacade projectFacade, ICommentService commentService, IUserProjectService userProjectService)
        {
            _ticketService = ticketService;
            _ticketHistoryService = ticketHistoryService;
            _appUserService = appUserService;
            _mapper = mapper;
            _projectFacade = projectFacade;
            _commentService = commentService;
            _userProjectService = userProjectService;
        }


        public async Task<IActionResult> Index(int id, string? sortOrder, string? searchString)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            ViewData["DateSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Date_Asc" : "";

            var project = await _projectFacade.FindProjectById(id);
            var user = await _projectFacade.GetUser(User);
            var userProjects = await _projectFacade.GetUserProjects(user);
            ICollection<Ticket> sortedTickets = project.Tickets;

            if (project == null) throw new KeyNotFoundException("Project could not be found in the database with the given ID");
            
            switch(sortOrder)
            {
                case "Date_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.CreatedAt).ToList();
                    break;
                case "Date_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case "Status_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.Status).ToList();
                    break;
                case "Status_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.Status).ToList();
                    break;
                case "Priority_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.Priority).ToList();
                    break;
                case "Priority_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.Priority).ToList();
                    break;
                case "Assigned_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.AssignedTo).ToList();
                    break;
                case "Assigned_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.AssignedTo).ToList();
                    break;
                case "Type":
                    sortedTickets = sortedTickets.OrderBy(x => x.Type).ToList();
                    break;

            }

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                sortedTickets = sortedTickets.Where(s => s.AssignedTo.ToLower().Contains(searchString)
                                       || s.CommentCount.ToString().Contains(searchString)
                                       || s.Id.ToString().ToLower().Contains(searchString)
                                       || s.Status.ToString().ToLower().Contains(searchString)
                                       || s.Title.ToString().ToLower().Contains(searchString)
                                       || s.Type.ToString().ToLower().Contains(searchString)
                                       || s.Priority.ToString().Contains(searchString)).ToList();

            }

            var vm = new DetailViewModel
            {
                Project = project,
                TicketCreateVm = new TicketCreateViewModel
                {
                    ProjectUsers = await _projectFacade.GetUserSelectListByProjectId(id),
                    ProjectId = id
                },
                OpenTicketCount = await _projectFacade.GetProjectTicketCountByStatus(id, Status.Open),
                ClosedTicketCount = await _projectFacade.GetProjectTicketCountByStatus(id, Status.Closed),
                User = user,
                UserProjectRole = _projectFacade.GetUserProjectRole(userProjects, project.ProjectId),
                Tickets = sortedTickets
            };



            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DetailViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }
            // Check if ticket is empty
            if (string.IsNullOrEmpty(vm.TicketCreateVm.Title) || string.IsNullOrEmpty(vm.TicketCreateVm.Description))
                return RedirectToAction("Index", "Ticket", new { id = vm.Project.ProjectId });



            var user = await _appUserService.GetUserByClaim(User);
            var ticket = _mapper.Map(vm.TicketCreateVm, new Ticket());
            var assignedToUser = await _appUserService.FindOne(vm.TicketCreateVm.AssignedToId);


            ticket.ProjectId = vm.Project.ProjectId;
            ticket.RequestorId = user.Id;
            ticket.Status = vm.TicketStatus;
            ticket.Priority = vm.TicketCreateVm.Priority;
            ticket.Type = vm.TicketCreateVm.Type;
            ticket.CreatedBy = user.FirstName + " " + user.LastName;
            ticket.AssignedTo = assignedToUser.FirstName + " " + assignedToUser.LastName;
            ticket.AssignedToId = assignedToUser.Id;
            await _ticketService.Create(ticket);

            return RedirectToAction("Index", "Ticket", new { id = ticket.ProjectId });
        }


        public async Task<IActionResult> Details(int id, string? searchString)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var ticket = await _ticketService.FindOne(id);
            var user = await _projectFacade.GetUser(User);
            var userProjects = await _projectFacade.GetUserProjects(user);
            var UserProjectRole = _projectFacade.GetUserProjectRole(userProjects, ticket.ProjectId);
            var collaborators = await _userProjectService.GetUserCollabsByProjectId(ticket.ProjectId);
            var ticketHistory = ticket.TicketHistory;
            var project = await _projectFacade.FindProjectById(ticket.ProjectId);

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                ticketHistory = ticketHistory.Where(s => s.NewValue.ToLower().Contains(searchString)
                                       || s.OldValue.ToLower().Contains(searchString)
                                       || s.Property.ToLower().Contains(searchString)
                                       || s.DateTime.ToString().Contains(searchString)).ToList();
            }


            if (ticket == null) throw new KeyNotFoundException("Ticket could not be found in the database with the given ID");

            var vm = new TicketCreateViewModel
            {
                Title = ticket.Title,
                Description = ticket.Description,
                ProjectId = ticket.ProjectId,
                Project = project,
                AssignedToId = ticket.AssignedToId,
                Ticket = ticket,
                TicketId = ticket.Id,
                Comments = ticket.Comments,
                User = user,
                UserProjectRole = UserProjectRole,
                TicketHistory = ticketHistory,
                userFullName = user.FirstName + " " + user.LastName,
                ProjectUsers = await _projectFacade.GetUserSelectListByProjectId(ticket.ProjectId),
                Attachments = ticket.Attachments

            };

            return View(vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostComment(TicketCreateViewModel tVm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var ticket = await _ticketService.FindOne(tVm.Ticket.Id);
            var user = await _projectFacade.GetUser(User);

            var comment = new Comment
            {
                CommentText = tVm.Comment.CommentText,
                FullName = $"{ user.FirstName } {  user.LastName }",
                User = user
            };


            await _commentService.Create(comment);

            ticket.Comments.Add(comment);

            ticket.CommentCount = ticket.Comments.Count();

            await _ticketService.Update(ticket);

            return RedirectToAction("Details", "Ticket", new { id = ticket.Id });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTicket(TicketCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var ticket = await _ticketService.FindOne(vm.Ticket.Id);
            var oldPriority = ticket.Priority;
            var oldStatus = ticket.Status;
            ticket.Priority = vm.Ticket.Priority;
            ticket.Status = vm.Ticket.Status;
            var oldAssignedTo = ticket.AssignedToId; 

            var oldAssignedUser = await _appUserService.FindOne(oldAssignedTo);
            var newAssignedUser = await _appUserService.FindOne(vm.AssignedToId);



            // Update ticket
            await _ticketService.Update(ticket);


            if (ticket.Priority != oldPriority)
            {
                // Add to ticket history
                var ticketHistory = new TicketHistory
                {
                    ProjectId = ticket.ProjectId,
                    TicketId = ticket.Id,
                    OldValue = oldPriority.ToString(),
                    NewValue = ticket.Priority.ToString(),
                    Property = "Priority "

                };
                await _ticketHistoryService.Create(ticketHistory);
                ticket.TicketHistory.Add(ticketHistory);
                await _ticketService.Update(ticket);
            }

            if (ticket.Status != oldStatus)
            {
                var ticketHistory = new TicketHistory
                {
                    ProjectId = ticket.ProjectId,
                    TicketId = ticket.Id,
                    OldValue = oldStatus.ToString(),
                    NewValue = ticket.Status.ToString(),
                    Property = "Status"

                };
                await _ticketHistoryService.Create(ticketHistory);
                ticket.TicketHistory.Add(ticketHistory);
                await _ticketService.Update(ticket);
            }

            if (vm.AssignedToId != oldAssignedTo && vm.AssignedToId != 0)
            {

                var ticketHistory = new TicketHistory
                {
                    ProjectId = ticket.ProjectId,
                    TicketId = ticket.Id,
                    OldValue = oldAssignedUser.FirstName + " " + oldAssignedUser.LastName,
                    NewValue = newAssignedUser.FirstName + " " + newAssignedUser.LastName,
                    Property = "Assigned To"
                };
                ticket.AssignedTo = newAssignedUser.FirstName + " " + newAssignedUser.LastName;
                ticket.AssignedToId = vm.AssignedToId;
                await _ticketHistoryService.Create(ticketHistory);
                ticket.TicketHistory.Add(ticketHistory);
                await _ticketService.Update(ticket);
            }

            return RedirectToAction("Details", "Ticket", new { id = ticket.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditTicket(TicketCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }
            var ticket = await _ticketService.FindOne(vm.Ticket.Id);

            ticket.Title = vm.Title;
            ticket.Description = vm.Description;
            ticket.Type = vm.Type;

            await _ticketService.Update(ticket);


            return RedirectToAction("Details", "Ticket", new { id = ticket.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(TicketCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var ticket = await _ticketService.FindOne(vm.Ticket.Id);
            await _projectFacade.RemoveTicketFKs(ticket.Id);
            await _ticketService.Delete(ticket.Id);
            return RedirectToAction("Index", "Ticket", new { id = ticket.ProjectId });
        }

        public async Task<IActionResult> UserTicketList(string? sortOrder)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            var user = await _projectFacade.GetUser(User);
            var sortedTickets = await _projectFacade.GetUserTickets(user);

            switch (sortOrder)
            {
                case "Date_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.CreatedAt).ToList();
                    break;
                case "Date_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case "Status_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.Status).ToList();
                    break;
                case "Status_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.Status).ToList();
                    break;
                case "Priority_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.Priority).ToList();
                    break;
                case "Priority_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.Priority).ToList();
                    break;
                case "Assigned_Asc":
                    sortedTickets = sortedTickets.OrderBy(x => x.AssignedTo).ToList();
                    break;
                case "Assigned_Desc":
                    sortedTickets = sortedTickets.OrderByDescending(x => x.AssignedTo).ToList();
                    break;
                case "Type":
                    sortedTickets = sortedTickets.OrderBy(x => x.Type).ToList();
                    break;

            }

            var vm = new TicketCreateViewModel
            {
                UserTicketList = sortedTickets,
                OpenTicketCount = await _projectFacade.GetUserTicketCountByStatus(user, Status.Open),
                ClosedTicketCount = await _projectFacade.GetUserTicketCountByStatus(user, Status.Closed),
                userFullName = user.FirstName + " " + user.LastName
            };

            return View(vm);
        }
    }
}
  
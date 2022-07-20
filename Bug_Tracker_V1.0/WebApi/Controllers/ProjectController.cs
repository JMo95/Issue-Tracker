using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Data;
using Microsoft.AspNetCore.Authorization;
using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.ViewModels.ProjectViewModels;
using Bug_Tracker_V1._0.Facades;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.ViewModels.AppUserViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bug_Tracker_V1._0.BuisinessLogic.Interfaces;
using Bug_Tracker_V1._0.WebApi.ViewModels.AppUserViewModels;
using Bug_Tracker_V1._0.WebApi.Controllers;

namespace Bug_Tracker_V1._0.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectFacade _projectFacade;
        private readonly IUserProjectService _userProjectService;
        private readonly IAppUserService _appUserService;
        private readonly INotificationService _notificationService;
        private readonly IProjectService _projectService;

        public ProjectController(IProjectFacade projectFacade, IUserProjectService userProjectService, IAppUserService appUserService, INotificationService notificationService, IProjectService projectService)
        {
            _projectFacade = projectFacade;
            _userProjectService = userProjectService;
            _appUserService = appUserService;
            _notificationService = notificationService;
            _projectService = projectService;
        }
        public async Task<IActionResult> Index()
        {
            return View(new IndexViewModel { 
                
                User = await _projectFacade.GetUser(User) 
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IndexViewModel vm)
        {
            // Check if the model state is valid
            // Server side validation
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }
            var user = await _projectFacade.GetUser(User);

            var project = new Project
            {
                ProjectName = vm.NewProjectName,
                ProjectDescription = vm.NewProjectDescription,
                ProjectOwnerId = user.Id,
                ProjectCreatedBy = $"{ user.FirstName } {  user.LastName }",
                ProjectCreatedTime = DateTime.Now,
                ProjectTeamSize = 1
        };

            await _projectFacade.CreateProject(project);

            var userProject = new UserProject
            {
                AuthenticationUserId = user.Id,
                ProjectId = project.ProjectId,
                AuthenticationUser = user,
                Project = project
            };

            await _projectFacade.CreateUserProject(userProject);

            return RedirectToAction("Index", "Project");

        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectFacade.FindProjectById(id);

            var collaborators = await _userProjectService.GetUserCollabsByProjectId(id);

            if (project == null) throw new KeyNotFoundException("Project was not found with provided id");

            project.ProjectTeamSize = _projectFacade.GetTeamSize(collaborators);
            await _projectService.Update(project);

            var vm = new DetailViewModel
            {
                Project = project,
                Collaborators = collaborators
            };
            return View(vm);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var project = await _projectFacade.FindProjectById(id);

            var vm = new DetailViewModel
            {
                Project = project
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitEdit(DetailViewModel vm)
        {
            // Need to find the project before I start updating it.
            // If Im using these new projects it won't update
            // Maybe use detail vm?
            var projectUpdateEntity = await _projectService.FindOne(vm.Project.ProjectId);

            projectUpdateEntity.ProjectName = vm.Project.ProjectName;
            projectUpdateEntity.ProjectDescription = vm.Project.ProjectDescription;

            await _projectService.Update(projectUpdateEntity);

            return RedirectToAction("Index", "Project");
        }

        public async Task<IActionResult> Collaborate(int id)
        {
            var nonCollaborators = await _appUserService.GetAll();
            var collaborators = await _userProjectService.GetUserCollabsByProjectId(id);
            nonCollaborators = nonCollaborators.Where(x => !collaborators.Contains(x));

            var user = await _projectFacade.GetUser(User);
            var role = user.UserProjects.First(x => x.ProjectId == id);
            var userRole = role.ProjectRole;

            var vm = new CollaborateViewModel
            {
                ProjectId = id,
                Collaborators = await _userProjectService.GetUserCollabsByProjectId(id),
                NonCollaborators = nonCollaborators.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.LastName + ", " + x.FirstName })
                                                   .OrderByDescending(x => x.Text),
                UserRole = userRole
            };
            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNotification(CollaborateViewModel vm)
        {
            List<String> selectedCollaborators = vm.SelectedCollaborators;
            //Saves the url with project id query so that after inviting user(s) the page gets reloaded to the same page.
            TempData["returnurl"] = Request.Headers["Referer"].ToString();

            //check if selected collabs is empty, if its not start adding the notification
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Project");
            }

            // This is causing non null reference error
            // fixed because I forgot to add the services for notification in startup file

            // To fix I has to pass the @html.hiddenfor(x => x.ProjectId) to save that property when it got passed back
            // here in the viewmodel
            var project = await _projectService.FindOne(vm.ProjectId);

            if (vm.SelectedCollaborators.Count > 0)
            {
                foreach (var user in vm.SelectedCollaborators)
                {

                    var notification = new Notification
                    {
                        AuthenticationUserId = Int32.Parse(user),
                        ProjectId = vm.ProjectId,
                        ProjectOwnerId = project.ProjectOwnerId,
                        NofiticationMessage = $"You have been invited to collaborate on {project.ProjectName} as a {vm.UserRole}",
                        UserProjectRole = vm.UserRole
                };

                    if (!(await _projectFacade.UnaknowledgedNotificationExistsForUser(notification)))
                    {
                        await _notificationService.Create(notification);
                        //Insert notification into users notifications.
                        // currentUser.Notifications.Add(notification);
                        //var CurrentUserId = Int32.Parse(user);
                        //var currentUser = _appUserService.FindOne(CurrentUserId);
                    }
                }
            }

            //ideally if someone clicks invite and the selected list is empty html will just display a message saying
            //please select the user that you want to invite.

            return Redirect(TempData["returnurl"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AcceptNotification(int id)
        {
            Notification notification = await _notificationService.FindOne(id);
            var projectId = notification.ProjectId;
            Project project = await _projectFacade.FindProjectById(projectId);
            var user = await _projectFacade.GetUser(User);
            var collabUsers = await _userProjectService.GetUserCollabsByProjectId(id);

            var userProject = new UserProject
            {
               // AuthenticationUserId = user.Id,
                ProjectId = project.ProjectId,
                AuthenticationUser = user,
                Project = project,
                ProjectRole = notification.UserProjectRole
            };

            await _userProjectService.Create(userProject);

            notification.isAcknowledged = true;
            await _notificationService.Update(notification);

            project.ProjectTeamSize = _projectFacade.GetTeamSize(collabUsers);
            await _projectService.Update(project);

            return RedirectToAction("Index", "Project");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeclineNotification(int id)
        {
            TempData["returnurl"] = Request.Headers["Referer"].ToString();
            Notification notification = await _notificationService.FindOne(id);
            notification.isAcknowledged = true;
            await _notificationService.Update(notification);
            return Redirect(TempData["returnurl"].ToString());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(int id)
        {
            await _projectFacade.RemoveProjectFKs(id);
            await _projectService.Delete(id);

            return RedirectToAction("Index", "Project");
        }

        }

}

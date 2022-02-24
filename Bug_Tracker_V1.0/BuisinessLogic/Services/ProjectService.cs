using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Models;
using Bug_Tracker_V1._0.Models.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;

        public ProjectService(IProjectRepository projectRepo)
        {
            _projectRepo = projectRepo;
        }

        public async Task<int> Create(Project entity)
        {
            return await _projectRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _projectRepo.Delete(id);
        }

        public async Task<Project> FindOne(int id)
        {
            return await _projectRepo.FindOne(id);
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _projectRepo.GetAll();
        }


        public async Task<IEnumerable<SelectListItem>> GetUsersAsSelectListItemsByProjectId(int id)
        {
            var project = await _projectRepo.FindOne(id);
            var list = new List<SelectListItem> { new SelectListItem { Value = "", Text = "Unassigned" } };

            if (project == null || project.UserProjects == null)
                return list;

            foreach (var userProject in project.UserProjects)
            {
                list.Add(new SelectListItem
                {
                    Value = userProject.AuthenticationUserId.ToString(),
                    Text = userProject.AuthenticationUser.LastName.ToString() + ", " + userProject.AuthenticationUser.FirstName.ToString()
                });
            }

            list = list.OrderBy(x => x.Text).ToList();

            return list;
        }

        public async Task<int> Update(Project entity)
        {
            return await _projectRepo.Update(entity);
        }
    }
}

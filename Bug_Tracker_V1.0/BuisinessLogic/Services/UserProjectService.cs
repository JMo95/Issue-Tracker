using Bug_Tracker_V1._0.Areas.Identity.Data;
using Bug_Tracker_V1._0.BuisinessLogic.Models;
using Bug_Tracker_V1._0.Interfaces;
using Bug_Tracker_V1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bug_Tracker_V1._0.Services
{
    public class UserProjectService : IUserProjectService
    {
        private readonly IUserProjectRepository _userProjectRepo;

        public UserProjectService(IUserProjectRepository userProjectRepo)
        {
            _userProjectRepo = userProjectRepo;
        }
        public async Task<int> Create(UserProject entity)
        {
            return await _userProjectRepo.Create(entity);
        }

        public async Task<int> Delete(int id)
        {
            return await _userProjectRepo.Delete(id);
        }

        public async Task<UserProject> FindOne(int id)
        {
            return await _userProjectRepo.FindOne(id);
        }

        public async Task<IEnumerable<UserProject>> GetAll()
        {
            return await _userProjectRepo.GetAll();
        }

        public async Task<IEnumerable<UserProject>> GetAllByUserId(int id)
        {
            var userProjects = await _userProjectRepo.GetAll();
            userProjects = userProjects.Where(x => x.AuthenticationUserId == id);

            if (userProjects == null)
                return new List<UserProject>();

            return userProjects;
        }

        public async Task<IEnumerable<AuthenticationUser>> GetUserCollabsByProjectId(int id)
        {
            var userProjects = await _userProjectRepo.GetAll();

            return userProjects.Where(x => x.ProjectId == id)
                               .Select(x => x.AuthenticationUser)
                               .Distinct();
        }

        public async Task<int> Update(UserProject entity)
        {
            return await _userProjectRepo.Update(entity);
        }

        public async Task<IEnumerable<AuthenticationUser>> GetCollaboratingUsersByProject(int projectId)
        {
            var userProjectList = await _userProjectRepo.GetAll();

            return userProjectList.Where(user => user.ProjectId == projectId)
                                  .Select(user => user.AuthenticationUser)
                                  .Distinct();
        }

        public async Task<IEnumerable<AuthenticationUser>> GetNonCollabUsersByProjectId(int id)
        {
            var userProjectList = await _userProjectRepo.GetAll();

            var collaborations = userProjectList.Where(user => user.ProjectId == id)
                                  .Select(user => user.AuthenticationUser)
                                  .Distinct();

            return userProjectList.Where(user => user.ProjectId != id && !collaborations.Contains(user.AuthenticationUser))
                                  .Select(user => user.AuthenticationUser)
                                  .Distinct();
        }

    }


}

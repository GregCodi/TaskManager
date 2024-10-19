using Microsoft.EntityFrameworkCore;
using TaskManager.API.Models.Abstractions;
using TaskManager.API.Models.Data;
using TaskManager.Common.Models;

namespace TaskManager.API.Models.Services
{
    public class ProjectsService : AbstractionService, ICommonService<ProjectModel>
    {
        private readonly ApplicationContext _db;

        public ProjectsService(ApplicationContext db)
        {
            _db = db;
        }

        public bool Create(ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {  
                Project newProject = new Project(model);
                _db.Projects.Add(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public bool Delete(int id)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
                _db.Projects.Remove(newProject);
                _db.SaveChanges();
            });
            return result;
        }
        //model or newProject
        public bool Update(int id, ProjectModel model)
        {
            bool result = DoAction(delegate ()
            {
                Project newProject = _db.Projects.FirstOrDefault(p => p.Id == id);
                newProject.Name = model.Name;
                newProject.Description = model.Description;
                newProject.Photo = model.Photo;
                newProject.Status = model.Status;
                newProject.AdminId = model.AdminId;
                _db.Projects.Update(newProject);
                _db.SaveChanges();
            });
            return result;
        }

        public ProjectModel Get(int id)
        { 
            Project project = _db.Projects.FirstOrDefault(p => p.Id == id);
            return project?.ToDto();
        }

        public async Task<IEnumerable<ProjectModel>> GetByUserId(int userId)
        {
            List<ProjectModel> result = new List<ProjectModel>();
            var admin = _db.ProjectAdmins.FirstOrDefault(a => a.UserId == userId);

            if(admin != null)
            {
                var projectsForAdmin = await _db.Projects.Where(p => p.AdminId == admin.Id).Select(p => p.ToDto()).ToListAsync();
                result.AddRange(projectsForAdmin);
            }

            var peojectsForUser = await _db.Projects.Include(p => p.AllUsers).Where(p => p.AllUsers.Any(u => u.Id == userId)).Select(p => p.ToDto()).ToListAsync();
            result.AddRange(peojectsForUser);
            return result;
        }

        public IQueryable<ProjectModel> GetAll()
        {
            return _db.Projects.Select(p => p.ToDto());
        }

        public void RemoveUsersFromProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.FirstOrDefault(p => p.Id ==  id);

            foreach(int userId in userIds) 
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                project.AllUsers.Add(user);
            }
            _db.SaveChanges(); 
        }

        public void RemoveUsersToProject(int id, List<int> userIds)
        {
            Project project = _db.Projects.Include(p => p.AllUsers).FirstOrDefault(p => p.Id == id);

            foreach (int userId in userIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == userId);
                if (project.AllUsers.Contains(user))
                    project.AllUsers.Add(user);
            }
            _db.SaveChanges();
        }
    }
}

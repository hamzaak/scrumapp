using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using Scrumapp.Data;
using Scrumapp.Data.Models;

namespace Scrumapp.Services.Concrete
{
    public class ProjectManager:IProjectService
    {
        private readonly ScrumappDbContext _context;

        public ProjectManager(ScrumappDbContext context)
        {
            _context = context;
        }

        public async Task<Project> GetByIdAsync(string id)
        {
            return await _context.Projects.FindAsync(id);

        }

        public IEnumerable<Project> GetAll()
        {
            return _context.Projects
                .Include(x => x.ProjectStatus);
        }

        public IEnumerable<Project> GetProjectsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId)
        {
            return _context.UserRoles.Where(x => x.ProjectId == projectId);
        }

        public IEnumerable<ProjectTask> GetProjectTasks(string projectId, string statusId)
        {
            return _context.ProjectTasks
                .Include(x => x.User)
                .Include(x => x.Project)
                .Include(x => x.Status)
                .Where(x => x.Project.Id == projectId && x.Status.Id == statusId);
        }

        public IEnumerable<ProjectTaskStatus> GetProjectTaskStatuses()
        {
            return _context.ProjectTaskStatuses;
        }

        public async Task Add(Project project)
        {
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Project project)
        {
            _context.Update(project);
            await _context.SaveChangesAsync();
        }

        public Task SetNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task SetDescriptionAsync(string description)
        {
            throw new NotImplementedException();
        }

        public async Task AddUser(ApplicationUserRole userRole)
        {
            await _context.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<ProjectTask> GetProjectTaskById(string id)
        {
            return await _context.ProjectTasks
                .Include(x=>x.Project)
                .Include(x=>x.User)
                .Include(x=>x.Status)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddTask(ProjectTask projectTask)
        {
            var taskStatus = await _context.ProjectTaskStatuses.FirstOrDefaultAsync(x => x.Id == "1");
            projectTask.Status = taskStatus;
            
            await _context.AddAsync(projectTask);
            
            var taskStatusLog = new ProjectTaskStatusLog
            {
                LogDate = DateTime.Now,
                ProjectTask = projectTask,
                Status = projectTask.Status
            };

            await _context.AddAsync(taskStatusLog);

            await _context.SaveChangesAsync();
        }

        public async Task StartTask(ProjectTask projectTask)
        {
            _context.Update(projectTask);

            var taskStatus = await _context.ProjectTaskStatuses.FirstOrDefaultAsync(x => x.Id == "2");
            projectTask.Status = taskStatus;

            var taskStatusLog = new ProjectTaskStatusLog
            {
                LogDate = DateTime.Now,
                ProjectTask = projectTask,
                Status = projectTask.Status
            };

            await _context.AddAsync(taskStatusLog);

            await _context.SaveChangesAsync();
        }

        public async Task VerifyTask(ProjectTask projectTask)
        {
            _context.Update(projectTask);

            var taskStatus = await _context.ProjectTaskStatuses.FirstOrDefaultAsync(x => x.Id == "3");
            projectTask.Status = taskStatus;

            var taskStatusLog = new ProjectTaskStatusLog
            {
                LogDate = DateTime.Now,
                ProjectTask = projectTask,
                Status = projectTask.Status
            };

            await _context.AddAsync(taskStatusLog);

            await _context.SaveChangesAsync();
        }

        public async Task CompleteTask(ProjectTask projectTask)
        {
            _context.Update(projectTask);

            var taskStatus = await _context.ProjectTaskStatuses.FirstOrDefaultAsync(x => x.Id == "4");
            projectTask.Status = taskStatus;

            var taskStatusLog = new ProjectTaskStatusLog
            {
                LogDate = DateTime.Now,
                ProjectTask = projectTask,
                Status = projectTask.Status
            };

            await _context.AddAsync(taskStatusLog);

            await _context.SaveChangesAsync();
        }

        public async Task AddTaskStatus(ProjectTaskStatus status)
        {
            await _context.ProjectTaskStatuses.AddAsync(status);
        }
    }
}

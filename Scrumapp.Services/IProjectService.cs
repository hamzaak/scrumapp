using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scrumapp.Data.Models;

namespace Scrumapp.Services
{
    public interface IProjectService
    {
        Task<Project> GetByIdAsync(string id);
        IEnumerable<Project> GetAll();
        IEnumerable<Project> GetProjectsByUserIdAsync(string userId);
        IEnumerable<ApplicationUserRole> GetProjectUserRoles(string projectId);

        IEnumerable<ProjectTask> GetProjectTasks(string projectId, string statusId);
        IEnumerable<ProjectTaskStatus> GetProjectTaskStatuses();

        Task Add(Project project);
        Task Update(Project project);
        Task SetNameAsync(string name);
        Task SetDescriptionAsync(string description);

        Task AddUser(ApplicationUserRole userRole);

        Task<ProjectTask> GetProjectTaskById(string id);
        Task AddTask(ProjectTask projectTask);
        Task StartTask(ProjectTask projectTask);
        Task VerifyTask(ProjectTask projectTask);
        Task CompleteTask(ProjectTask projectTask);

        Task AddTaskStatus(ProjectTaskStatus status);
    }
}

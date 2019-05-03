using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scrumapp.Data.Models;
using Scrumapp.Services;
using Scrumapp.WebMvcUI.Models.ProjectViewModels;
using Scrumapp.WebMvcUI.Models.RoleViewModels;
using Scrumapp.WebMvcUI.Models.ScrumViewModels;
using Scrumapp.WebMvcUI.Models.UserViewModels;

namespace Scrumapp.WebMvcUI.Controllers
{
    public class ScrumController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ScrumController(IProjectService projectService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager):base(userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            var model = new BoardViewModel
            {
                ProjectId = id,
                ProjectName = project.Name,
                BacklogTasks = _projectService.GetProjectTasks(id, "1"),
                InProgressTasks = _projectService.GetProjectTasks(id, "2"),
                TestTasks = _projectService.GetProjectTasks(id, "3"),
                CompletedTasks = _projectService.GetProjectTasks(id, "4")
            };
            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddTask(string id)
        {
            var model = new BoardAddTaskViewModel
            {
                ProjectId = id,
                Users = new List<ProjectUserDetailViewModel>()
            };

            var users = _userManager.Users.Select(user => new UserDetailViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ImageUrl = user.ImageUrl
            });

            var roles = _roleManager.Roles
                .Where(x => x.Name != "Admin")
                .OrderBy(x => x.Name)
                .Select(r => new ApplicationRoleDetailViewModel
                {
                    Id = r.Id,
                    RoleName = r.Name,
                    Description = r.Description
                });


            var userRoles = _projectService.GetProjectUserRoles(id);
            foreach (var userRole in userRoles)
            {
                var user = await users.FirstOrDefaultAsync(x => x.Id == userRole.UserId);
                var role = await roles.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                var projectUser = new ProjectUserDetailViewModel
                {
                    ProjectId = id,
                    UserId = userRole.UserId,
                    RoleId = userRole.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ImageUrl = user.ImageUrl,
                    UserRoleName = role.RoleName
                };
                model.Users.Add(projectUser);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTask(BoardAddTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var project = await _projectService.GetByIdAsync(model.ProjectId);
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.UserId);

            var projectTaskStatus = _projectService.GetProjectTaskStatuses().Where(x => x.Id == "1").ToList()[0];
            var projectTask = new ProjectTask
            {
                Project = project,
                User = user,
                Content = model.Content,
                Duration = new TimeSpan(0,model.Hour,model.Minute,0),
                Status = projectTaskStatus
            };

            await _projectService.AddTask(projectTask);
            
            return RedirectToAction(nameof(Index), new{project.Id});
        }
        
        public async Task<IActionResult> StartTask(string id)
        {
            var task = await _projectService.GetProjectTaskById(id);
            
            await _projectService.StartTask(task);

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        public async Task<IActionResult> VerifyTask(string id)
        {
            var task = await _projectService.GetProjectTaskById(id);

            await _projectService.VerifyTask(task);

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }

        public async Task<IActionResult> CompleteTask(string id)
        {
            var task = await _projectService.GetProjectTaskById(id);

            await _projectService.CompleteTask(task);

            return RedirectToAction(nameof(Index), new { task.Project.Id });
        }
    }
}
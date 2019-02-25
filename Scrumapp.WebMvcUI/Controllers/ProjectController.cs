using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scrumapp.Data.Models;
using Scrumapp.Services;
using Scrumapp.WebMvcUI.Models.ProjectViewModels;
using Scrumapp.WebMvcUI.Models.RoleViewModels;
using Scrumapp.WebMvcUI.Models.UserViewModels;

namespace Scrumapp.WebMvcUI.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public ProjectController(IProjectService projectService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager):base(userManager)
        {
            _projectService = projectService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {
            var projects = _projectService.GetAll().Select(x => new ProjectDetailViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            var model = new ProjectIndexViewModel
            {
                Projects = projects
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _projectService.Add(new Project
            {
                Name = model.Name,
                Description = model.Description,
                CreatedDate = DateTime.Now
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var project = await _projectService.GetByIdAsync(id);
            if (project == null)
            {
                throw new ApplicationException($"Unable to load project with ID '{id}'.");
            }

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

            var model = new ProjectDetailViewModel
            {
                Id = id,
                Name = project.Name,
                Description = project.Description,
                ProjectUsers = new List<ProjectUserDetailViewModel>(),
                Users = users,
                Roles = roles
            };

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
                model.ProjectUsers.Add(projectUser);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var projectId = model.Id;
            var project = await _projectService.GetByIdAsync(projectId);
            if (project == null)
            {
                ModelState.AddModelError("Error", "Unable to load the project");
                return View(model);
            }

            project.Name = model.Name;
            project.Description = model.Description;

            await _projectService.Update(project);

            model.StatusMessage = $"{project.Name} project has been updated";

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser(ProjectUserDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Error", "Error");
                return RedirectToAction(nameof(Edit), new { model.ProjectId });
            }

            var userRole = new ApplicationUserRole
            {
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                RoleId = model.RoleId
            };

            await _projectService.AddUser(userRole);

            return RedirectToAction(nameof(Edit), new { id = model.ProjectId });
        }
    }
}
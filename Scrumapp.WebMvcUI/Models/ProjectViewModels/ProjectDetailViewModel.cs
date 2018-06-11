using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Scrumapp.WebMvcUI.Models.RoleViewModels;
using Scrumapp.WebMvcUI.Models.UserViewModels;

namespace Scrumapp.WebMvcUI.Models.ProjectViewModels
{
    public class ProjectDetailViewModel
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string StatusMessage { get; set; }

        public IList<ProjectUserDetailViewModel> ProjectUsers { get; set; }
        public IEnumerable<UserDetailViewModel> Users { get; set; }
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }
    }
}

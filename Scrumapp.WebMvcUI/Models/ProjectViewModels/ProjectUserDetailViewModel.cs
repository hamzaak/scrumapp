using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scrumapp.WebMvcUI.Models.ProjectViewModels
{
    public class ProjectUserDetailViewModel
    {
        [Required]
        public string ProjectId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string UserRoleName { get; set; }

    }
}

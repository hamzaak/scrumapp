using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrumapp.WebMvcUI.Models.RoleViewModels
{
    public class ApplicationRoleDetailViewModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string UserRoleName { get; set; }
        public string StatusMessage { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrumapp.WebMvcUI.Models.RoleViewModels
{
    public class ApplicationRoleIndexViewModel
    {
        public IEnumerable<ApplicationRoleDetailViewModel> Roles { get; set; }
    }
}

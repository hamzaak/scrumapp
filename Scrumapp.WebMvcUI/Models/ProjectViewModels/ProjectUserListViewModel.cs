using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scrumapp.WebMvcUI.Models.ProjectViewModels
{
    public class ProjectUserListViewModel
    {
        public IEnumerable<ProjectUserDetailViewModel> ProjectUsers { get; set; }
    }
}

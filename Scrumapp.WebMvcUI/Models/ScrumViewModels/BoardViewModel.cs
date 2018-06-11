using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Scrumapp.Data.Models;

namespace Scrumapp.WebMvcUI.Models.ScrumViewModels
{
    public class BoardViewModel
    {
        public string ProjectId { get; set; }
        public string ProjectName { get; set; }

        public IEnumerable<ProjectTask> BacklogTasks { get; set; }
        public IEnumerable<ProjectTask> InProgressTasks { get; set; }
        public IEnumerable<ProjectTask> TestTasks { get; set; }
        public IEnumerable<ProjectTask> CompletedTasks { get; set; }

    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Scrumapp.WebMvcUI.Models.ProjectViewModels;

namespace Scrumapp.WebMvcUI.Models.ScrumViewModels
{
    public class BoardAddTaskViewModel
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public int Hour { get; set; }

        [Required]
        public int Minute { get; set; }

        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string UserId { get; set; }

        public IList<ProjectUserDetailViewModel> Users { get; set; }
    }
}

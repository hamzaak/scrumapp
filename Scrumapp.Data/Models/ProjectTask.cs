using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Scrumapp.Data.Models
{
    public class ProjectTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public TimeSpan Duration { get; set; }

        public virtual Project Project { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ProjectTaskStatus Status { get; set; }
    }
}

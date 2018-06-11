using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Scrumapp.Data.Models
{
    public class ProjectTaskStatusLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual ProjectTask ProjectTask { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual ProjectTaskStatus Status { get; set; }
        
        public DateTime LogDate { get; set; }
    }
}

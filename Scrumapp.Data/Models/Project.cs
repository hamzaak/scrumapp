using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scrumapp.Data.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public ProjectStatus ProjectStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime CompletedDate { get; set; } 
        
        public virtual IEnumerable<ProjectTask> Tasks { get; set; }
    }
}

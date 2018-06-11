using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Scrumapp.Data.Identity.Models
{
    public abstract class User :IdentityUser
    {
        [Required]
        [StringLength(50,ErrorMessage = "Maximum 50 characters")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Maximum 50 characters")]
        public string LastName { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public bool Status { get; set; }
    }
}

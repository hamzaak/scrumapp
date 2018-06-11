using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Scrumapp.Data.Models
{
    public class Role:IdentityRole
    {
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scrumapp.Data.Models;

namespace Scrumapp.Data
{
    public class ScrumappDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string,ApplicationUserClaim,ApplicationUserRole, ApplicationUserLogin,ApplicationRoleClaim,ApplicationUserToken>
    {
        public ScrumappDbContext(DbContextOptions<ScrumappDbContext> options) : base(options) { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectStatus> ProjectStatuses { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<ProjectTaskStatus> ProjectTaskStatuses { get; set; }
        public DbSet<ProjectTaskStatusLog> ProjectTaskStatusLogs { get; set; }

    }
}

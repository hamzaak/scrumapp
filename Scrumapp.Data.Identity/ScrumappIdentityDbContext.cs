using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scrumapp.Data.Identity.Models;

namespace Scrumapp.Data.Identity
{
    public class ScrumappIdentityDbContext : IdentityDbContext<User>
    {
        public ScrumappIdentityDbContext(DbContextOptions options)
            : base(options) { }
        
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Scrumapp.Data.Models;

namespace Scrumapp.Services
{
    public interface IApplicationUserService
    {
        void Add(ApplicationUser applicationUser);
    }
}

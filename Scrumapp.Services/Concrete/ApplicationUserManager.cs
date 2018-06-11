using Microsoft.AspNetCore.Identity;
using Scrumapp.Data.Models;

namespace Scrumapp.Services.Concrete
{
    public class ApplicationUserManager:IApplicationUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async void Add(ApplicationUser applicationUser)
        {
            await _userManager.CreateAsync(applicationUser);
        }
    }
}

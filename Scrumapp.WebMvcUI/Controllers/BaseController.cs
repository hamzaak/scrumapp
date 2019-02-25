using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scrumapp.Data.Models;
using System.Linq;

namespace Scrumapp.WebMvcUI.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BaseController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                filterContext.Result = new RedirectResult(Url.Action("Login", "Account"));
            }
        }

    }
}
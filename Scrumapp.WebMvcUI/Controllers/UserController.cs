using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scrumapp.Data.Models;
using Scrumapp.WebMvcUI.Models.AccountViewModels;
using Scrumapp.WebMvcUI.Models.ManageViewModels;
using Scrumapp.WebMvcUI.Models.UserViewModels;

namespace Scrumapp.WebMvcUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public IActionResult Index()
        {

            var users = _userManager.Users.Select(user => new UserDetailViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });

            var model = new UserIndexViewModel
            {
                Users = users
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Email = model.Email
                };

                //_applicationUserService.Add(user);

                var result = await _userManager.CreateAsync(user, model.Password);
                if (!result.Succeeded)
                {
                    AddErrors(result);
                    
                }

                if (model.IsAdmin)
                {
                    var newRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                    if (!newRoleResult.Succeeded)
                    {
                        AddErrors(newRoleResult);
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x=>x.Id == id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user.");
            }

            var isAdminExists = await _userManager.IsInRoleAsync(user, "Admin");

            var model = new UserEditViewModel
            {
                Id=user.Id,
                IsAdmin = isAdminExists,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsEmailConfirmed = user.EmailConfirmed,
                StatusMessage = StatusMessage
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user.");
            }

            //Remove exist role
            var isAdminExists = await _userManager.IsInRoleAsync(user, "Admin");
            if (isAdminExists)
            {
                var roleResult = await _userManager.RemoveFromRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                {
                    throw new ApplicationException($"Unable to remove user role.");
                }
            }

            if (model.IsAdmin)
            {
                var newRoleResult = await _userManager.AddToRoleAsync(user, "Admin");
                if (!newRoleResult.Succeeded)
                {
                    throw new ApplicationException($"Unable to add new user role.");
                }
            }

            var firstName = user.FirstName;
            var lastName = user.LastName;
            if (model.FirstName != firstName || model.LastName != lastName)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                var setFirstNameResult = await _userManager.UpdateAsync(user);
                if (!setFirstNameResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting firstname and lastname for user with ID '{user.Id}'.");
                }
            }

            var email = user.Email;
            if (model.Email != email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            var phoneNumber = user.PhoneNumber;
            if (model.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }
            
            StatusMessage = "Your profile has been updated";
            return RedirectToAction(nameof(Index));
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        
        #endregion
    }
}
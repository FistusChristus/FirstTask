using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTask.Data;
using FirstTask.Models.DbModels;
using FirstTask.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstTask.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        RoleManager<CustomRole> _roleManager;
        UserManager<CustomUser> _userManager;
        public RolesController(RoleManager<CustomRole> roleManager, UserManager<CustomUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public IActionResult Index() => View(_roleManager.Roles.AsNoTracking().ToArray());

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new CustomRole() { Name = name });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            CustomRole role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }

        public IActionResult UserList() => View(_userManager.Users.ToList());

        public async Task<IActionResult> Edit(string userId)
        {
            // получаем пользователя
            CustomUser customUser = await _userManager.FindByIdAsync(userId);
            if (customUser != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(customUser);
                var roleNames = _roleManager.Roles.Select(r => r.Name).ToArray();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = customUser.Id,
                    UserEmail = customUser.Email,
                    UserRoles = userRoles,
                    RoleNames = roleNames,
                };
                return View(model);
            }

            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string userId, List<string> roles)
        {
            // получаем пользователя
            CustomUser customUser = await _userManager.FindByIdAsync(userId);
            if (customUser != null)
            {
                // получем список ролей пользователя
                var userRoles = await _userManager.GetRolesAsync(customUser);
                // получаем все роли
                var allRoles = _roleManager.Roles.AsNoTracking().ToArray();
                // получаем список ролей, которые были добавлены
                var addedRoles = roles.Except(userRoles);
                // получаем роли, которые были удалены
                var removedRoles = userRoles.Except(roles);

                await _userManager.AddToRolesAsync(customUser, addedRoles);

                await _userManager.RemoveFromRolesAsync(customUser, removedRoles);

                return RedirectToAction("UserList");
            }

            return NotFound();
        }
    }
}

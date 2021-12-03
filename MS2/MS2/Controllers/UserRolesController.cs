﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MS2.Data;
using MS2.Data.Entities;
using MS2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Controllers
{
    public class UserRolesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISiteRepository _repository;
        public UserRolesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ISiteRepository repo)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repo;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("Manager") && !User.IsInRole("Owner"))
                return View("AccessDenied");

            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRolesViewModel>();
            foreach (ApplicationUser user in users)
            {
                var thisViewModel = new UserRolesViewModel();
                thisViewModel.UserId = user.Id;
                thisViewModel.Email = user.Email;
                thisViewModel.Roles = await GetUserRoles(user);
                userRolesViewModel.Add(thisViewModel);
            }
            return View(userRolesViewModel);
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public async Task<IActionResult> Manage(string userId)
        {
            if (!User.IsInRole("Manager") && !User.IsInRole("Owner"))
                return View("AccessDenied");

            ViewBag.userId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }
            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles.ToList())
            {
                if (User.IsInRole("Manager") && !User.IsInRole("Owner"))
                {
                    if (role.Name == "Owner" || role.Name == "Manager" || role.Name == "Customer")
                        continue;
                }

                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, IFormCollection form)
        {


            var user = await _userManager.FindByIdAsync(form["UserId"]);
            if (user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y => y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult CreateEmployee()
        {
            if (!User.IsInRole("Manager") && !User.IsInRole("Owner"))
                return View("AccessDenied");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(IFormCollection form)
        {
            if (!User.IsInRole("Manager") && !User.IsInRole("Owner"))
                return View("AccessDenied");
            const string DEFAULT_PASS = "123Pa$$word.";
            ApplicationUser user = new ApplicationUser()
            {
                UserName = form["Email"],
                Email = form["Email"],
                EmailConfirmed = true,
                PhoneNumberConfirmed = false,
                FirstName = form["FirstName"],
                LastName = form["LastName"],
                CreatedAt = DateTime.Now
            };
            IdentityResult createdUser = await _userManager.CreateAsync(user, DEFAULT_PASS);
            if (createdUser.Succeeded)
            {
                IdentityResult createdRole = await _userManager.AddToRoleAsync(user, form["Role"]);
                if (createdRole.Succeeded)
                    return await Dashboard();
            }
            return View("CreateEmployeeFailure");
        }

        [HttpPost]
        public async Task<IActionResult> Terminate(IFormCollection form)
        {
            if (!User.IsInRole("Manager") && !User.IsInRole("Owner"))
                return View("AccessDenied");

            ApplicationUser user = await _userManager.FindByIdAsync(form["Id"]);
            foreach (string role in await _userManager.GetRolesAsync(user))
            {
                if (role != "Customer")
                    await _userManager.RemoveFromRoleAsync(user, role);
            }
            user.Status = "Employee Termination. Reason: " + form["Reason"];
            return await Dashboard();
        }

        public async Task<IActionResult> Dashboard()
        {
            ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            List<string> roles = (List<string>)await _userManager.GetRolesAsync(user);

            if (roles.Contains("Owner"))
            {
                return View("Owner", _userManager.Users.Where(u => u.Id != user.Id));
            }

            if (roles.Contains("Manager"))
            {
                return View("Manager", _userManager.Users.Where(u => u.Id != user.Id));
            }

            if (roles.Contains("Cashier"))
            {
                return View("Cashier");
            }

            if (roles.Contains("Cook"))
            {
                return View("Cook");
            }
            if (roles.Contains("Driver"))
            {
                var driversOrders = _repository.GetOrdersByDriverId(user.Id);
                return View("Driver", driversOrders.Where(o=> o.Status == OrderStatus.OutForDelivery.ToString()).OrderBy(o => o.OrderDate));
            }
            return Redirect("~/");
        }
    }
}

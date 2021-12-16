using Microsoft.AspNetCore.Identity;
using MS2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data
{
    public class AuthSeeder
    {
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cashier.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Driver.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cook.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string DEFAULT_PASS = "123Pa$$word.";

            var ownerUser = new ApplicationUser() { UserName = "owner@hgpizza.com", Email = "owner@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "FirstName", LastName = "LastName", Address="1111", CreatedAt = DateTime.Now};
            await userManager.CreateAsync(ownerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(ownerUser, Roles.Owner.ToString());

            var customerUser = new ApplicationUser() { UserName = "customer@hgpizza.com", Email = "customer@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "FirstName", LastName = "LastName", Address = "1111", CreatedAt = DateTime.Now };
            await userManager.CreateAsync(customerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(customerUser, Roles.Customer.ToString());

            var cashierUser = new ApplicationUser() { UserName = "cashier@hgpizza.com", Email = "cashier@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "FirstName", LastName = "LastName", Address = "1111", CreatedAt = DateTime.Now };
            await userManager.CreateAsync(cashierUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(cashierUser, Roles.Cashier.ToString());

            var driverUser = new ApplicationUser() { UserName = "driver@hgpizza.com", Email = "driver@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "DriverFirstName", LastName = "LastName", Address = "1111", CreatedAt = DateTime.Now };
            await userManager.CreateAsync(driverUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(driverUser, Roles.Driver.ToString());

            var cookUser = new ApplicationUser() { UserName = "cook@hgpizza.com", Email = "cook@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "FirstName", LastName = "LastName", Address = "1111", CreatedAt = DateTime.Now };
            await userManager.CreateAsync(cookUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(cookUser, Roles.Cook.ToString());

            ApplicationUser managerUser = new ApplicationUser() { UserName = "manager@hgpizza.com", Email = "manager@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true, PhoneNumber = "5155142500", FirstName = "FirstName", LastName = "LastName", Address = "1111", CreatedAt = DateTime.Now };
            await userManager.CreateAsync(managerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(managerUser, Roles.Manager.ToString());
        }
    }
}

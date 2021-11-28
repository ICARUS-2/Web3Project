using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS2.Data
{
    public class AuthSeeder
    {
        public static async Task SeedRolesAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Customer.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cashier.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Driver.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Cook.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
        }

        public static async Task SeedUsersAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            const string DEFAULT_PASS = "123Pa$$word.";

            IdentityUser ownerUser = new IdentityUser() { UserName = "owner@hgpizza.com", Email = "owner@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true};
            await userManager.CreateAsync(ownerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(ownerUser, Roles.Owner.ToString());
            await userManager.AddToRoleAsync(ownerUser, Roles.Cook.ToString());

            IdentityUser customerUser = new IdentityUser() { UserName = "customer@hgpizza.com", Email = "customer@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            await userManager.CreateAsync(customerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(customerUser, Roles.Customer.ToString());

            IdentityUser cashierUser = new IdentityUser() { UserName = "cashier@hgpizza.com", Email = "cashier@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            await userManager.CreateAsync(cashierUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(cashierUser, Roles.Cashier.ToString());

            IdentityUser driverUser = new IdentityUser() { UserName = "driver@hgpizza.com", Email = "driver@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            await userManager.CreateAsync(driverUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(driverUser, Roles.Driver.ToString());

            IdentityUser cookUser = new IdentityUser() { UserName = "cook@hgpizza.com", Email = "cook@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            await userManager.CreateAsync(cookUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(cookUser, Roles.Cook.ToString());

            IdentityUser managerUser = new IdentityUser() { UserName = "manager@hgpizza.com", Email = "manager@hgpizza.com", EmailConfirmed = true, PhoneNumberConfirmed = true };
            await userManager.CreateAsync(managerUser, DEFAULT_PASS);
            await userManager.AddToRoleAsync(managerUser, Roles.Manager.ToString());
        }
    }
}

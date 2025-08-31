using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Loan_Management.Models;
using System.Reflection.Metadata;
using Microsoft.VisualBasic;

namespace Loan_Management
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            await EnsureRolesAsync(roleManager);
            await EnsureFinanceRolesAsync(roleManager);

            var userManager = service.GetRequiredService<UserManager<ApplicationUser>>();
            await EnsureTestAdminAsync(userManager);
            await EnsureTesFinanceAsync(userManager);
        }
        //Create admin user if they don't exist
        private static async Task EnsureTestAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var testAdmin = await userManager.Users.Where(x => x.UserName == "admin@loans.local").SingleOrDefaultAsync();
            if (testAdmin != null) return;

            testAdmin = new ApplicationUser //inherits from IdentityUser
            {
                UserName = "admin@loans.local",
                Email = "admin@loans.local"
            };

            await userManager.CreateAsync(testAdmin, "NotSecure123!!");

            await userManager.AddToRoleAsync(testAdmin, Constants.AdminstratorRole);


        }
        private static async Task EnsureTesFinanceAsync(UserManager<ApplicationUser> userManager)
        {
            var testFinance = await userManager.Users.Where(x => x.UserName == "finance@loans.local").SingleOrDefaultAsync();
            if (testFinance != null) return;

            testFinance = new ApplicationUser
            {
                UserName = "finance@loans.com",
                Email = "finance@loans.com"
            };

            await userManager.CreateAsync(testFinance, "NotSecure123!!");
            await userManager.AddToRoleAsync(testFinance, Constants.FinanceRole);
        }
        //assign the created admin user adminstartor role
        private static async Task EnsureRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var alreadyExists = await roleManager.RoleExistsAsync(Constants.AdminstratorRole);

            if (alreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.AdminstratorRole));

            
        }

        private static async Task EnsureFinanceRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            var financealreadyExists = await roleManager.RoleExistsAsync(Constants.FinanceRole);

            if (financealreadyExists) return;

            await roleManager.CreateAsync(new IdentityRole(Constants.FinanceRole));
        }
    }
}
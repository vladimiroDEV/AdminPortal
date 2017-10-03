using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using AdminPortalWeb.Models;
using Microsoft.Extensions.Logging;

namespace AdminPortalWeb.Data
{
    public class DbInitializer
    {
      

        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
                                             RoleManager<IdentityRole> roleManager, ILogger<DbInitializer> logger)
        {
           
            context.Database.EnsureCreated();

            // Look for any users.
            //if (context.Users.Any())
            //{
            //    return; // DB has been seeded
            //}



            //creating role
                if (!context.Roles.Any(r => r.Name == "Administrator"))
                await roleManager.CreateAsync(new IdentityRole("Administrator"));
            if (!context.Roles.Any(r => r.Name == "AppUser"))
                await roleManager.CreateAsync(new IdentityRole("AppUser"));
            


            //Create the default Admin account and apply the Administrator role
            if((await userManager.FindByEmailAsync("c.vladimiro88@gmail.com")) == null)
            {
                string user = "c.vladimiro88@gmail.com";
                string password = "V_ladimiro17";
                var result =  await userManager.CreateAsync(new ApplicationUser { UserName = user, Email = user, EmailConfirmed = true }, password);

                if (result.Succeeded)
                {

                    var newUser = await userManager.FindByNameAsync(user);
                    var res = await userManager.AddToRoleAsync(newUser, "Administrator");
                }
               
               
            }
        }

    }
}

using Microsoft.AspNetCore.Identity;
using RealState.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealState.Infrastructure.Identity
{
    public static class AppIdentityDbContextSeed
    {

        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(userManager.Users.Count() == 0)
            {
                var user = new AppUser()
                {
                    Name = "Ibrahim Mohamed",
                    Email = "hemasayed600@gmail.com",
                    UserName = "IbrahimMohamed",
                    CreatedAt = DateTime.Now,
                    PhoneNumber = "01116191349"
                };

                await userManager.CreateAsync(user, "Pa$$w0rd");
            }

            if (!roleManager.RoleExistsAsync("Admin").GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole("Admin")).Wait();
                roleManager.CreateAsync(new IdentityRole("Customer")).Wait();
            }
        }

    }
}

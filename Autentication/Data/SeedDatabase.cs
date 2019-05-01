using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Autentication.Data
{
    public class SeedDatabase
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            string adminUsername = "admin";
            string adminPassword = "admin123";

            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = adminUsername,
                };
                await userManager.CreateAsync(user);
                await userManager.AddPasswordAsync(user, adminPassword);
            }
        }
    }
}

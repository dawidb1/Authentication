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
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                ApplicationUser user = new ApplicationUser()
                {
                    Email = "admin@biometria.pl",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = "admin",
                    FirstName = "Dawid",
                    LastName = "Baranski",
                    Status = false
                };
                await userManager.CreateAsync(user);
                await userManager.AddPasswordAsync(user, "admin123");
            }
        }
    }
}

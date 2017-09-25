using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Data;
using System;
using Xunit;

namespace Server.Test
{
    public class UnitTest : Assert
    {
        public IServiceProvider Services { get; }

        public UnitTest()
        {
            Services = WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration(config => config.AddJsonFile("appsettings.Development.json"))
                .UseStartup<Startup>()
                .Build()
                .Services;
        }

        [Fact]
        public async void AddTestUser()
        {
            UserManager<ApplicationUser> userManager = Services.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = "a@a", Email = "a@a" };
            await userManager.CreateAsync(user, "Admin12345.");
            await userManager.AddToRoleAsync(user, Roles.ADMIN);
        }
    }
}

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Data;
using Server.Pages.Account;
using Server.Services;
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
        public void Test()
        {
            NotNull(Services.GetService<ApplicationDbContext>());
        }

        [Fact]
        public async void TestRegister()
        {
            var model = new RegisterModel(
                Services.GetService<UserManager<ApplicationUser>>(),
                Services.GetService<SignInManager<ApplicationUser>>(),
                Services.GetService<ILogger<LoginModel>>(),
                Services.GetService<IEmailSender>());

            model.Input = new RegisterModel.InputModel
            {
                Email = "a@a",
                Password = "Admin12345.",
                ConfirmPassword = "Admin12345."
            };

            model.Challenge();

            var result = await model.OnPostAsync();
        }
    }
}

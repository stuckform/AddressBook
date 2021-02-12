using AddressBook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace AddressBook.Utilities
{
    public class DataUtility
    {


        public static string GetConnectionString(IConfiguration configuration)
        {
            //The default connection string will come from appSettings like usual
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            //It will be automatically overwritten if we are running on Heroku
            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            //Provides an object representation of a uniform resource identifier (URI) and easy access to the parts of the URI
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            //Provides a simple way to create and manage the contents of connection strings used by the NpgsqlConnection class.
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };

            return builder.ToString();

        }

        public static async Task ManageDataAsync(IHost host)
        {
            //This Technique is used to obtain refrences to services that get registered in the
            //ConfigureServices method in the startup class
            using var svcScope = host.Services.CreateScope();
            var svcProvider = svcScope.ServiceProvider;

            // This dbContextSvc knows how to talk to the DB (aka _context)
            //Service 1: an Incstance of Application DbContext
            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            //Service 2: An instance of RoleManager
            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Service 3: an instance of UserManager
            var userManagerSvc = svcProvider.GetRequiredService<UserManager<IdentityUser>>();

            //This is the programmict equivalent to Update-Database       
            await dbContextSvc.Database.MigrateAsync();
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleSvc)

        {
            //write the code to seed a few Roles
            //Call upon the roleSvc to add a new Role
            await roleSvc.CreateAsync(new IdentityRole("Administrator"));
            await roleSvc.CreateAsync(new IdentityRole("Moderator"));

        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManagerSvc)
        {
            //Write the code to see a few Users
            //Step 1: Create yourself as a user
            var adminUser = new IdentityUser()
            {
                Email = "Coppinger.dev@gmail.com",
                UserName = "Coppinger.dev@gmail.com",            
                EmailConfirmed = true
            };

            await userManagerSvc.CreateAsync(adminUser, "Num1811418!");

            //Step 2 : Create Someone else as a user
            var modUser = new IdentityUser()
            {
                Email = "Mod@mail.com",
                UserName = "Mod@mail.com",
                EmailConfirmed = true
            };

            await userManagerSvc.CreateAsync(modUser, "Abc&123!");
        }

        private static async Task AssignRolesAsync(UserManager<IdentityUser> userManagerSvc)
        {
            //Step 1: Somehow get a reference to the Coppinger.dev user
            var adminUser = await userManagerSvc.FindByEmailAsync("Coppinger.dev@gmail.com");

            //Step 2: Assign the adminUser to the Administrator role
            await userManagerSvc.AddToRoleAsync(adminUser, "Administrator");

            //Step 3: step 1 and 2 again but for moderator
            var modUser = await userManagerSvc.FindByEmailAsync("Mod@mail.com");
            await userManagerSvc.AddToRoleAsync(modUser, "Moderator");
        }


    }
}

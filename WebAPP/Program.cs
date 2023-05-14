using WebAPP.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebAPP.Areas.Identity.Data;
using System.Text.Json.Serialization;

namespace WebAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
                        var connectionString = builder.Configuration.GetConnectionString("WebAPPContextConnection") ?? throw new InvalidOperationException("Connection string 'WebAPPContextConnection' not found.");

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            
            builder.Services.AddDbContext<WebAPPContext>();

            builder.Services.AddDefaultIdentity<UserAccount>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;

                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@_-.";
                options.User.RequireUniqueEmail = false;
            }).AddEntityFrameworkStores<WebAPPContext>();

            // ???
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Services.AddDbContext<WebAPPContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();;
            app.UseAuthorization();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<WebAPPContext>();
                dbContext.Database.OpenConnection();
                var connection = dbContext.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA journal_mode = WAL;";
                command.ExecuteNonQuery();

                dbContext.Database.EnsureCreated();
                DBInitializer.Initialize(dbContext); // add data if needed
            }

            app.Run();
        }
    }
}
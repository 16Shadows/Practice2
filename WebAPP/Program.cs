using Microsoft.EntityFrameworkCore;

namespace WebAPP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddDbContext<DMOrganizerDBContext>();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DMOrganizerDBContext>();
                dbContext.Database.OpenConnection();
                var connection = dbContext.Database.GetDbConnection();
                using var command = connection.CreateCommand();
                command.CommandText = "PRAGMA schema.journal_mode = WAL;";
                command.ExecuteNonQuery();
                dbContext.Database.EnsureCreated();
            }

            app.Run();
        }
    }
}
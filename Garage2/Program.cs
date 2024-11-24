using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Garage301.Data;
using Microsoft.AspNetCore.Identity;
using Garage301.Models;
using Garage301.Extentions;
namespace Garage301
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<Garage301Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Garage301Context") ?? throw new InvalidOperationException("Connection string 'Garage301Context' not found.")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Garage301Context>();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            await app.SeedDataAsync();


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapRazorPages(); // Ensure Razor Pages are mapped

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapControllerRoute(
                name: "admin",
                pattern: "{controller=Admin}/{action=ParkingSpots}/{id?}");
            app.Run();
        }
    }
}

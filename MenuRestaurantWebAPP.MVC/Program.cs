using MenuRestaurantWebAPP.Contexts;
using MenuRestaurantWebAPP.ContextServices;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using MenuRestaurantWebAPP.Models;

namespace MenuRestaurantWebAPP.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // var authDBconnectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");
            var menurestaurantDBconnectionString = builder.Configuration.GetConnectionString("MenuRestaurantDbContextConnection") ?? throw new InvalidOperationException("Connection string 'MenuRestaurantDbContextConnection' not found.");
            var authDBconnectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");


            builder.Services.AddDbContext<MenuRestaurantDbContext>(options => options.UseSqlServer(menurestaurantDBconnectionString, x => x.MigrationsAssembly("MenuRestaurantWebAPP.Contexts")));
            builder.Services.AddDbContext<AuthDbContext>(options => options.UseSqlServer(authDBconnectionString, x => x.MigrationsAssembly("MenuRestaurantWebAPP.Contexts")));
            builder.Services.ConfigureApplicationCookie(options => {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.ReturnUrlParameter = "";
            });

            builder.Services.Configure<IdentityOptions>(options => {
                options.User.AllowedUserNameCharacters += " ";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                
            });

            builder.Services.AddDefaultIdentity<AuthUser>
                (options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.AddScoped<IMenuRestaurantDbContextService, MenuRestaurantDbContextService>();


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            builder.Services.AddProgressiveWebApp("manifest.json");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapRazorPages();

            app.Run();
        }
    }
}

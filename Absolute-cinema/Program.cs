using DataAccessObjects;
using Repositories;
using Services;

namespace Absolute_cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllersWithViews();
            
            // Add our repos
            SetupRepos(builder);
            // Add our services
            SetupServices(builder);

            // Configure the database to allow one request accessing the same context
            builder.Services.AddSqlServer<AbsoluteCinemaContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
            
            // Allow page to access the session directly
            builder.Services.AddHttpContextAccessor(); 
            // Add session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = System.TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = true;
            });
            

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

            app.UseSession();
            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
        
        private static void SetupServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserService, UserService>();
        }
        
        private static void SetupRepos(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserDetailRepository, UserDetailRepository>();
        }
    }
}
﻿using DataAccessObjects;
using Repositories;
using Services;
using Services.Interfaces;
using Services.Implementations;
using Hangfire;
using Services.HelperServices;
using Services.BackgroundServices.Showtimes;
using Common.Models;
using Common.DTOs.CloudinaryDTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Services.VNPay;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Common.DTOs.Gemini;
using Services.Gemini;
using Microsoft.AspNetCore.Authentication.Google;


namespace Absolute_cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configure EmailSettings
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            // Add our repos
            SetupRepos(builder);
            // Add our services
            SetupServices(builder);
            // Background services
            SetupBackgroundServices(builder);

            // Configure Cloudinary
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("Cloudinary"));
            builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();

            // Configure Gemini
            builder.Services.Configure<GeminiAIConfig>(builder.Configuration.GetSection("GeminiAI"));

            // Configure the database to allow one request accessing the same context
            builder.Services.AddSqlServer<AbsoluteCinemaContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

            // Configure Hangfire to use SQL Server storage.
            builder.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Start the Hangfire Server to process background jobs.
            builder.Services.AddHangfireServer();

            // Allow page to access the session directly
            builder.Services.AddHttpContextAccessor();

            // Add session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = System.TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            // Configure authentication with cookies
            // Cookie Authentication setup
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login"; // redirect if not logged in
                    options.AccessDeniedPath = "/Account/AccessDenied"; // redirect if not authorized
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                })
                .AddGoogle(options =>
                {
                    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
                    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
                    options.CallbackPath = "/Account/signin-google";
                });

            // Debug
            Console.WriteLine($"WORKING ENVIRONMENT: {builder.Environment.EnvironmentName}");
            Console.WriteLine($"SQL string: {builder.Configuration.GetConnectionString("DefaultConnection")}");

            var app = builder.Build();

            // Expose the Hangfire Dashboard at /hangfire (be sure to secure this in production).
            app.UseHangfireDashboard();
            RunBackgroundJobs(app);

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Config notfound-forbidden
            app.UseStatusCodePagesWithReExecute("/Errors/{0}");


            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }

        private static void RunBackgroundJobs(WebApplication app)
        {
            RecurringJob.AddOrUpdate<ShowtimeBackgroundJob>(
                "expire-old-showtimes",
                job => job.ExpireOldShowtimes(),
                Cron.Hourly); // Can be change as needed
        }

        private static void SetupBackgroundServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ShowtimeBackgroundJob>();
        }

        private static void SetupServices(WebApplicationBuilder builder)
        {
            // Email Service
            builder.Services.AddScoped<IEmailService, EmailService>();
            //Connection VNPay API
            builder.Services.AddScoped<IVnPayService, VnPayService>();
            // User Services
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserDetailService, UserDetailService>();

            // Movie Services
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActorService, ActorService>();
            builder.Services.AddScoped<IDirectorService, DirectorService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<IMovieActorService, MovieActorService>();
            builder.Services.AddScoped<IMovieDirectorService, MovieDirectorService>();
            builder.Services.AddScoped<IMovieTagService, MovieTagService>();

            // Room and Seat Services
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<ISeatService, SeatService>();
            builder.Services.AddScoped<ISeatTypeService, SeatTypeService>();

            // Showtime Services
            builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
            builder.Services.AddScoped<IShowtimeSeatService, ShowtimeSeatService>();
            // Search Services for showtimes
            builder.Services.AddTransient<SearchShowtimeByRoomNameStrategy>();
            builder.Services.AddTransient<SearchShowtimeByMovieNameStrategy>();
            builder.Services.AddTransient<SearchShowtimeByRoomNameAndMovieNameStrategy>();
            builder.Services.AddTransient<ShowtimeSearchContext>();

            // Booking and Payment Services
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<ITicketService, TicketService>();

            // Review Services
            builder.Services.AddScoped<IReviewService, ReviewService>();

            // Password services
            builder.Services.AddScoped<IHashPasswordService, HashPasswordService>();
            // Db init services
            builder.Services.AddScoped<DbInitializer>();
            // Facade
            builder.Services.AddScoped<IDashboardFacade, DashboardFacade>();

            // GeminiService
            builder.Services.AddHttpClient<IAIAssistantService, AIAssistantService>();
            builder.Services.AddScoped<IAIAssistantService, AIAssistantService>();

        }

        private static void SetupRepos(WebApplicationBuilder builder)
        {
            // User Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserDetailRepository, UserDetailRepository>();

            // Movie Repositories
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            builder.Services.AddScoped<IActorRepository, ActorRepository>();
            builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IMovieActorRepository, MovieActorRepository>();
            builder.Services.AddScoped<IMovieDirectorRepository, MovieDirectorRepository>();
            builder.Services.AddScoped<IMovieTagRepository, MovieTagRepository>();

            // Room and Seat Repositories
            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<ISeatRepository, SeatRepository>();
            builder.Services.AddScoped<ISeatTypeRepository, SeatTypeRepository>();

            // Showtime Repositories
            builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
            builder.Services.AddScoped<IShowtimeSeatRepository, ShowtimeSeatRepository>();

            // Booking and Payment Repositories
            builder.Services.AddScoped<IBookingRepository, BookingRepository>();
            builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
            builder.Services.AddScoped<ITicketRepository, TicketRepository>();

            // Review Repositories
            builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
        }
    }
}

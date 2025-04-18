﻿using ASC.DataAccess.Interface;
using ASC.WEB.Configuration;
using ASC.WEB.Data;
using ASC.WEB.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ASC.WEB.Services
{
   
        public static class DependencyInjection
        {
        // Config services
        public static IServiceCollection AddCongfig(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
            services.AddOptions();
            services.Configure<ApplicationSettings>(config.GetSection("AppSettings"));

            services.AddAuthentication()
                .AddGoogle(options =>
                {
                    IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
                    options.ClientId = config["Google:Identity:ClientId"];
                    options.ClientSecret = config["Google:Identity:ClientSecret"];
                });
            return services;
        }

        // Add service
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
            {
                // Add ApplicationDbContext
                services.AddScoped<DbContext, ApplicationDbContext>();

                // Add IdentityUser
                services.AddIdentity<IdentityUser, IdentityRole>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


                // Add services
                services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, AuthMessageSender>();
                services.AddTransient<ISmsSender, AuthMessageSender>();
                services.AddSingleton<IIdentitySeed, IdentitySeed>();
                services.AddScoped<IUnitOfWork, UnitOfWork>();
                services.AddSession();
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddDistributedMemoryCache(); //
                services.AddSingleton<INavigationCacheOperations, NavigationCacheOperations>();


            // Add RazorPages, MVC
            services.AddRazorPages();
                services.AddDatabaseDeveloperPageExceptionFilter();
                services.AddControllersWithViews();

                return services;
            }
        }
    }
   


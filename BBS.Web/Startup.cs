using BBS.Core.CronJobs;
using BBS.Core.Services;
using BBS.Core.Services.CronJobs;
using BBS.DAL.Clients.Abstract;
using BBS.DAL.Clients.Concrete;
using BBS.DAL.Database;
using BBS.DAL.Repositories;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Logger;
using BBS.Models.Models;
using BBS.Web.Constants;
using BBS.Web.Extensions;
using BBS.Web.Filters;
using BBS.Web.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.AutoRegisterDi;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Claims;

namespace BBS.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ITripClient, TripClient>();
            services.AddScoped<ITicketClient, TicketClient>();
            services.AddScoped<IBusDestinationClient, BusDestinationClient>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddControllersWithViews();
            services.AddMvc(config =>
                {
                    config.Filters.Add(typeof(CustomExceptionHandler));
                });

            services.AddDbContext<BankDbContext>(o =>
                o.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging());

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<BankDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = Cookies.AuthenticationCookieName;
                });

            services.AddAuthorization(config =>
            {
                config.DefaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim(ClaimTypes.Name)
                .Build();

                config.AddPolicy(Policies.AdminOnly, p => p.RequireRole(Roles.Admin));
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/UserAccount/AccessDenied";// when we have access denied view add it here!
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = "/UserAccount/Login";
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
            });

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(DepositTypeRepository)))
              .Where(c => c.Name.EndsWith("Repository"))
              .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(DepositService)))
             .Where(c => c.Name.EndsWith("Service"))
             .AsPublicImplementedInterfaces();

            services.AddSingleton<ILogger, Log>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddCronJob<LoanCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = ConvertIntToCronExpression.GetCronExpression();
            });

            services.AddCronJob<DepositCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = ConvertIntToCronExpression.GetCronExpression();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<BankDbContext>();
                dbContext.Database.Migrate();
                dbContext.SeedData();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStatusCodePagesWithRedirects("/Error/{0}");
            app.UseStaticFiles();

            app.UseRouting();

            //app.usecoo
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                                "Administration",
                                "Administration",
                                "Administration/{controller=CustomLoan}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

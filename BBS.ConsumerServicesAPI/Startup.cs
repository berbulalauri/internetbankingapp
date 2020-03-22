using BBS.ConsumerServicesAPI.DAL;
using BBS.ConsumerServicesAPI.Repositories.concrete;
using BBS.ConsumerServicesAPI.Services;
using BBS.Interfaces;
using BBS.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetCore.AutoRegisterDi;
using System.Linq;
using System.Reflection;

namespace BBS.ConsumerServicesAPI
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
            services.AddDbContext<ConsumerServiceDbContext>(p => p.UseSqlServer(Configuration.GetConnectionString("CSConnection")));
            services.AddControllers().AddNewtonsoftJson();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(TripRepository)))
              .Where(c => c.Name.EndsWith("Repository"))
              .AsPublicImplementedInterfaces();

            services.RegisterAssemblyPublicNonGenericClasses(Assembly.GetAssembly(typeof(TripService)))
              .Where(c => c.Name.EndsWith("Service"))
              .AsPublicImplementedInterfaces();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

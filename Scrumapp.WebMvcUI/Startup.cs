using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrumapp.Data;
using Scrumapp.Data.Models;
using Scrumapp.Services;
using Scrumapp.Services.Concrete;
using Scrumapp.WebMvcUI.Utilities;


namespace Scrumapp.WebMvcUI
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
            services.AddMvc();
            services.AddSingleton(Configuration);

            services.AddScoped<IApplicationUserService, ApplicationUserManager>();
            services.AddScoped<IProjectService, ProjectManager>();

            services.AddScoped<IFileService, FileManager>();

            var useInMemoryDatabase = Configuration.GetValue<bool>("UseInMemoryDatabase");
            if(useInMemoryDatabase)
            {
                services.AddDbContext<ScrumappDbContext>(options => options.UseInMemoryDatabase());
            }
            else
            {
                services.AddDbContext<ScrumappDbContext>(options =>
                   options.UseSqlServer(Configuration.GetConnectionString("ScrumappDbConnection")));
            }
            
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ScrumappDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options => options.LoginPath = "/Account/Login");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            
            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

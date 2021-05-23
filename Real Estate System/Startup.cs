using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Real_Estate_System.Data;
using Real_Estate_System.Models;
using Real_Estate_System.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Real_Estate_System
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
            services.AddMvc(option => option.EnableEndpointRouting = false);
            //Add (Dependency injection) Of Repoistory Design Patter

            services.AddScoped<RealStateRepository<Sales>, SalesEfRepository>();
            services.AddScoped<RealStateRepository<Engineer>, EngineerEfRepository>();
            services.AddScoped<RealStateRepository<Employess_and_workers>, EmployeesandworkersEfRepository>();
            services.AddScoped<RealStateRepository<Dublex>, DublexEfRepository>();
            services.AddScoped<RealStateRepository<Department>, DepartmentEfRepository>();
            services.AddScoped<RealStateRepository<CallCenter>, CallCenterEfRepository>();
            services.AddScoped<RealStateRepository<Client>, ClientEfRepository>();
            services.AddCloudscribePagination();


            services.AddDbContext<RealStateDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlCon")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<IdentityUser,IdentityRole>()
                .AddEntityFrameworkStores<RealStateDbContext>().AddDefaultUI().AddDefaultTokenProviders();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMvcWithDefaultRoute();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

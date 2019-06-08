using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DoxFlorisMVCWeb.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoxFlorisMVCWeb
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
            
            services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddDefaultUI(Microsoft.AspNetCore.Identity.UI.UIFramework.Bootstrap4).AddEntityFrameworkStores<DoxFlorisMVCWebContext>();
            services.AddDbContext<DoxFlorisMVCWebContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DoxFlorisMVCWebConnection")));
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes => 
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}");
        });

            CreateUserRoles(services).Wait();


        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var Context = serviceProvider.GetRequiredService<DoxFlorisMVCWebContext>();

            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if(!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            var user = Context.Users.FirstOrDefault(u => u.Email == "admin1@gmail.com");
            if( user != null)
            {
                var roles = Context.UserRoles;
                var adminRole = Context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if(adminRole != null)
                {
                    if(!roles.Any(ur => ur.UserId == user.Id && ur.RoleId == adminRole.Id))
                    {
                        roles.Add(new IdentityUserRole<string>() { UserId = user.Id, RoleId = adminRole.Id });
                        Context.SaveChanges();
                    }
                }
            }
        }
        
}
    }
    

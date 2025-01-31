using System;
using DoxFlorisMVCWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DoxFlorisMVCWeb.Areas.Identity.IdentityHostingStartup))]
namespace DoxFlorisMVCWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<DoxFlorisMVCWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DoxFlorisMVCWebContextConnection")));

               // services.AddDefaultIdentity<IdentityUser>()
                 //   .AddEntityFrameworkStores<DoxFlorisMVCWebContext>();
            });
        }
    }
}
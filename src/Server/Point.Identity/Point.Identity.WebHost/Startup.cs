using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Point.Identity.Infrastructure;
using Point.Identity.Infrastructure.Data;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Point.Identity.WebHost
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

            services.AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/Identification/Login";
                options.UserInteraction.LogoutUrl = "/Identification/Logout";
            })
            .AddAspNetIdentity<IdentityUser>()
            .AddInMemoryApiResources(IdentityConfiguration.GetApiResources())
            .AddInMemoryClients(IdentityConfiguration.GetClients())
            .AddInMemoryIdentityResources(IdentityConfiguration.GetIdentityResources())
            .AddInMemoryApiScopes(IdentityConfiguration.GetScopes())
            .AddDeveloperSigningCredential();

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddCors();

            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            dbInitializer.InitializeDb();
        }
    }
}

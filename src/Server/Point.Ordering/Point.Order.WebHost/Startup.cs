using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Point.Ordering.Infrastructure.Data;
using Point.Ordering.Infrastructure.Repositories;
using Point.Ordering.WebHost.GraphQL;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Point.Ordering.WebHost
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
            services.AddControllers();

            services.AddDbContext<OrderContext>(options =>
            {
                options.UseNpgsql(_config.GetConnectionString("Default"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services
                .AddGraphQLServer()
                .AddQueryType<IssuePointQueries>()
                .AddMutationType<IssuePointMutations>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
        }

        public void Configure(IApplicationBuilder app, 
            IWebHostEnvironment env,
            IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
   
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapGraphQL("/graphql");
            });

            dbInitializer.InitializeDb();
        }
    }
}

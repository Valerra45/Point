using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Point.Admin.Infrastructure.Data;
using Point.Admin.Infrastructure.Repositories;
using Point.Admin.WebHost.GraphQL.IssuePoints;
using Point.Contracts;
using Point.Ordering.WebHost.GraphQL.IssuePoints;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Point.Admin.WebHost
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

            services.AddDbContext<AdminContext>(options =>
            {
                options.UseNpgsql(_config.GetConnectionString("Default"));
                options.UseLazyLoadingProxies();
            });

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddMassTransit(x =>
            {

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    var massTransitSection = _config.GetSection("MassTransit");
                    var url = massTransitSection.GetValue<string>("Url");
                    var host = massTransitSection.GetValue<string>("Host");
                    var userName = massTransitSection.GetValue<string>("UserName");
                    var password = massTransitSection.GetValue<string>("Password");

                    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                    {
                        configurator.Username(userName);
                        configurator.Password(password);
                    });

                    cfg.Message<IIssuePointContract>(cfg => { });

                    cfg.AutoDelete = true;

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            services.AddMassTransitHostedService();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddGraphQLServer()
                .AddQueryType<IssuePointQuery>()
                .AddMutationType<IssuePointMutation>()
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
                //endpoints.MapControllers();

                endpoints.MapGraphQL("/graphql");
            });

            dbInitializer.InitializeDb();
        }
    }
}

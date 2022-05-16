using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Point.Contracts;
using Point.Ordering.Infrastructure.Consumers;
using Point.Ordering.Infrastructure.Data;
using Point.Ordering.Infrastructure.Repositories;
using Point.Ordering.WebHost.GraphQL.IssuePoints;
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

            services.AddMassTransit(x =>
            {
                x.AddConsumer<CreateOrderConsumer>();
                x.AddConsumer<OrderingCreateIssuePointConsumer>(_ => new OrderingCreateIssuePointConsumerDefinition());

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

                    cfg.Message<IOrderStatusContract>(cfg => { });

                    cfg.AutoDelete = true;

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            services.AddMassTransitHostedService();

            services.AddAutoMapper(typeof(Startup).Assembly);

            services
                .AddGraphQLServer()
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
                endpoints.MapGraphQL("/graphql");
            });

            dbInitializer.InitializeDb();
        }
    }
}

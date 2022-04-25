using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Point.Contracts;
using Point.External.Infrastructure.Consumers;
using Point.External.Infrastructure.Data;
using Point.External.Infrastructure.Environment;
using Point.External.Infrastructure.Repositories;
using Point.External.Infrastructure.Services.Points.Queryes;
using Point.SharedKernel.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Point.External.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ExternalDatabaseSettings>(
         _configuration.GetSection(nameof(ExternalDatabaseSettings)));

            services.AddSingleton<IExternalDatabaseSettings>(options =>
                options.GetRequiredService<IOptions<ExternalDatabaseSettings>>().Value);

            services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddControllers();

            services.AddOpenApiDocument(options =>
            {
                options.Title = "Point External API Doc";
                options.Version = "1.0";
            });

            services.AddMediatR(typeof(GetAllIssuePointsQuery).Assembly);

            services.AddAutoMapper(typeof(Startup).Assembly);

            services.AddMassTransit(x =>
            {
                x.AddConsumer<ExternalCreateIssuePointConsumer>(_ => new ExternalCreateIssuePointConsumerDefinition());

                x.UsingRabbitMq((ctx, cfg) =>
                {
                    var massTransitSection = _configuration.GetSection("MassTransit");
                    var url = massTransitSection.GetValue<string>("Url");
                    var host = massTransitSection.GetValue<string>("Host");
                    var userName = massTransitSection.GetValue<string>("UserName");
                    var password = massTransitSection.GetValue<string>("Password");

                    cfg.Host($"rabbitmq://{url}/{host}", configurator =>
                    {
                        configurator.Username(userName);
                        configurator.Password(password);
                    });

                    cfg.Message<IOrderContract>(cfg => { });

                    cfg.AutoDelete = true;

                    cfg.ConfigureEndpoints(ctx);
                });
            });

            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();
            app.UseSwaggerUi3(x =>
            {
                x.DocExpansion = "list";
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });

            dbInitializer.InitializeDb();
        }
    }
}

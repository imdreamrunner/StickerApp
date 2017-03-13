using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using MySQL.Data.Entity.Extensions;
using Swashbuckle.AspNetCore.Swagger;

using StickerApp.Misc;
using StickerApp.Services;

namespace StickerApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"appsettings.Local.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                // To enaable it, add the following dependency:
                // <PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.0.0" />
                // builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            // services.AddApplicationInsightsTelemetry(Configuration);

            services.Configure<ApplicationConfiguration>(Configuration);

            var connection = Configuration["DatabaseConnection"];
            services.AddDbContext<StickerDb>(options => options.UseMySQL(connection));

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionHandleFilter));
            });

            // Add token checking filter here so that it can read the application's configuration by dependency injection.
            services.AddScoped<TokenCheckingFilterAttribute>();

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new Info { Title = "StickerApp API", Version = "v1" });
                config.OperationFilter<AuthResponsesSwaggerOperationFilter>();
                config.OperationFilter<JsonResponseSwaggerOperationFilter>();
                // Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "StickerApp.xml");
                config.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "StickerApp API V1");
                config.ShowRequestHeaders();
                config.ShowJsonEditor();
            });
        }
    }
}
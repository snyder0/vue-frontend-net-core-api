using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using StarterApi.Services;
using StarterApi.Infrastructure.Mediatr;
using Swashbuckle.AspNetCore.Swagger;
using AutoMapper;
using StarterApi.Data;
using Microsoft.EntityFrameworkCore;
using StarterApi.Infrastructure;
using System.Text;
using System;
using StarterApi.Common.Constants;
using StarterApi.Security.Policies;
using Microsoft.AspNetCore.Authorization;

namespace StarterApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // Logging
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Validation
            AssemblyScanner.FindValidatorsInAssemblyContaining<Startup>().ForEach(pair => {
                services.Add(ServiceDescriptor.Scoped(pair.InterfaceType, pair.ValidatorType));
                services.Add(ServiceDescriptor.Scoped(pair.ValidatorType, pair.ValidatorType));
            });

            // Mediatr
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatingBehavior<,>));
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IMediatorService, MediatorService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            SetupDatabase(services);
            SetupAuthentication(services);
            SetupAuthorization(services);
            SetupCustomDependencies(services);
        }

        private void SetupDatabase(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opts =>
                opts.UseSqlServer(Configuration["ConnectionString:DefaultConnection"]));

            //services.AddDbContext<DataContext>(opt =>
            //    opt.UseInMemoryDatabase("StarterProjectInMemoryDb"));
        }

        private void SetupAuthentication(IServiceCollection services)
        {
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddJwtAuthentication(key);
            services.AddSwaggerDocumentation();
        }

        private void SetupAuthorization(IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyConstants.Policies.IsAdmin, policy =>
                    policy.Requirements.Add(new IsAdminRequirement()));
            });

            services.AddSingleton<IAuthorizationHandler, IsAdminHandler>();
        }

        private void SetupCustomDependencies(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Logging
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerDocumentation();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ParentEspoir.Persistence;
using MediatR;
using MediatR.Pipeline;
using ParentEspoir.Application;
using System.Reflection;
using FluentValidation.AspNetCore;
using ParentEspoir.Application.Infrastructure;
using ParentEspoir.WebUI;
using ParentEspoir.Common;
using ParentEspoir.Infrastructure;
using ParentEspoir.Domain.Entities;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using System.Collections.Generic;

namespace WebUI
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient<IDateTime, MachineDateTime>();

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestLogBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestCacheBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));
            services.AddMediatR(typeof(GetCustomerDescriptionQueryHandler).GetTypeInfo().Assembly);

            services.AddDbContextPool<ParentEspoirDbContext>(
                options =>
                options.UseMySql(Configuration.GetConnectionString("ParentEspoirMySQLDatabaseDev"),
                config => config.DisableBackslashEscaping()
                )
            );

            services.AddIdentity<AppUser, IdentityRole>(option =>
            {
                option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                option.Lockout.MaxFailedAccessAttempts = 5;
                option.Lockout.AllowedForNewUsers = false;
            })
            .AddEntityFrameworkStores<ParentEspoirDbContext>()
            .AddDefaultUI(UIFramework.Bootstrap4)
            .AddDefaultTokenProviders();

            services.AddMemoryCache();

            services
             .AddMvc()
             .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
             .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateCustomerModelValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Customer}/{action=Index}/{id?}");
            });

            new UserSetup(
                services.GetRequiredService<UserManager<AppUser>>(), 
                services.GetRequiredService<RoleManager<IdentityRole>>()
            ).Do();

            GenerateTestingData.Generate(services.GetRequiredService<ParentEspoirDbContext>());
        }
    }
}

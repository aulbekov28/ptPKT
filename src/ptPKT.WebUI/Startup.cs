using Askmethat.Aspnet.JsonLocalizer.Extensions;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ptPKT.Core.Entities.Identity;
using ptPKT.Infrastructure.Data;
using ptPKT.SharedKernel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using ptPKT.WebUI.Mappings;
using ptPKT.WebUI.Middlewares;

namespace ptPKT.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDatabase(Configuration.GetConnectionString(nameof(AppDbContext)));
            //services.AddEntityFrameworkNpgsql();
            //services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString(nameof(AppDbContext))));

            services.AddLocalization($"{Environment.WebRootPath}/Resources/");

            services.AddJwtAuthorization();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"});
            });
            return BuildDependencyInjectionProvider(services);
        }

        private static IServiceProvider BuildDependencyInjectionProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.Populate(services);

            var webAssembly = Assembly.GetExecutingAssembly();
            var coreAssembly = Assembly.GetAssembly(typeof(AppRole));
            var sharedAssembly = Assembly.GetAssembly(typeof(BaseEntity));
            var infrastructureAssembly = Assembly.GetAssembly(typeof(EfRepository)); // TODO: Move to Infrastucture Registry

            builder.RegisterAssemblyTypes(webAssembly,
                                          coreAssembly,
                                          sharedAssembly,
                                          infrastructureAssembly)
                   .AsImplementedInterfaces();

            // builder.RegisterType<IStringLocalizerFactory>().As<JsonStringLocalizerFactory>().SingleInstance();
            // builder.RegisterType<IStringLocalizer>().As<JsonStringLocalizer>().SingleInstance();

            // Automapper
            var profiles = from t in typeof(MappingProfile).Assembly.GetTypes()
                           where typeof(Profile).IsAssignableFrom(t)
                           select (Profile)Activator.CreateInstance(t);

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();

            var container = builder.Build();

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseCustomMiddlewaresPipeline();

            app.EnableSwagger();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }

    internal static class StartupExtensions
    {
        public static void AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        }

        public static void AddLocalization(this IServiceCollection services, string resourcePath)
        {
            services.AddJsonLocalization(option =>
            {
                option.CacheDuration = TimeSpan.FromDays(1);
                option.ResourcesPath = resourcePath;
                option.SupportedCultureInfos = new HashSet<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("ru-RU"),
                    new CultureInfo("jp-JP"),
                    new CultureInfo("kz-KZ"),
                };
            });
        }

        public static void UseCustomMiddlewaresPipeline(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void EnableSwagger(this IApplicationBuilder app)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

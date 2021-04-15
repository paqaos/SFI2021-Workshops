using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFI.Microservice.Users.Configuration;
using SFI.Microservice.Users.DatabaseLayer;
using SimpleInjector;
using Microsoft.EntityFrameworkCore;
using SFI.Microservice.Users.BusinessLayer.Mappers;

namespace SFI.Microservice.Users
{
    public class Startup
    {
        private Container _container;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _container = new Container();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<UsersDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("UsersDbContext"), b => b.MigrationsAssembly("SFI.Microservice.Users")));

            services.AddAutoMapper(config => config.AddProfile<UserProfile>());
            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();

                // Ensure activation of a specific framework type to be created by
                // Simple Injector instead of the built-in configuration system.
                // All calls are optional. You can enable what you need. For instance,
                // ViewComponents, PageModels, and TagHelpers are not needed when you
                // build a Web API.();
            });
            SimpleInjectorConfiguration.ConfigureSimpleInjector(_container);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MovieDatabase", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SFI.Microservice.Users v1"));
            }

            app.UseSimpleInjector(_container);
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

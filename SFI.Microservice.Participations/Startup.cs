using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SFI.Microservice.Participants.Configuration;
using SFI.Microservice.Participations.DatabaseLayer.Mappers;
using SimpleInjector;

namespace SFI.Microservice.Participants
{
    public class Startup
    {
        private Container _container = new Container();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string ManagedNetworkingAppContextSwitch = "Switch.Microsoft.Data.SqlClient.UseManagedNetworkingOnWindows";
            AppContext.SetSwitch(ManagedNetworkingAppContextSwitch, true);

            services.AddControllers();

            services.AddMvc()
                    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = null)
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    });

            services.AddAutoMapper(config => config.AddProfile<ParticipantsProfile>());

            //services.AddSimpleInjector(_container, options =>
            //{
            //    options.AddAspNetCore().AddControllerActivation();

            //    // Ensure activation of a specific framework type to be created by
            //    // Simple Injector instead of the built-in configuration system.
            //    // All calls are optional. You can enable what you need. For instance,
            //    // ViewComponents, PageModels, and TagHelpers are not needed when you
            //    // build a Web API.();
            //});
            SimpleInjectorConfiguration.ConfigureSimpleInjector(_container);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SFI.Microservice.Participations", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SFI.Microservice.Participations v1"));
            }

            //app.UseSimpleInjector(_container);
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

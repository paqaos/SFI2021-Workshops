using System;
using System.IO;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SFI.Microservice.Common.BusinessLayer.Services;
using SFI.Microservice.Events.Configuration;
using SFI.Microservice.Events.DatabaseLayer;
using SimpleInjector;
using SFI.Microservice.Events.BusinessLayer;
using SFI.Microservice.Events.Dto.Validators;

namespace SFI.Microservice.Events
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

            services.AddDbContext<EventsDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:EventsDbContext"]));

            services.AddMvc()
                    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = null)
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    })
                    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(typeof(CreateEventValidator).Assembly));

            services.AddMemoryCache();
            services.AddStackExchangeRedisCache(options => options.Configuration = Configuration["ConnectionStrings:Redis"]);
       
            services.AddAutoMapper(config => config.AddProfile<EventProfile>());

            services.AddHttpClient("Users", client => client.BaseAddress = new Uri(Configuration["Services:Users"]));

            services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
            });

            SimpleInjectorConfiguration.ConfigureSimpleInjector(_container, Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SFI.Microservice.Events", Version = "v1" });

                var filePath = Path.Combine(AppContext.BaseDirectory, "SFI.Microservice.Events.xml");
                if (File.Exists(filePath))
                    c.IncludeXmlComments(filePath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SFI.Microservice.Events v1"));

            app.UseSimpleInjector(_container);

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            _container.GetInstance<IAzureServiceBusHandler>();
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FMApp.Application.Weather.Interfaces;
using FMApp.Weather;
using FMApp.Application.Weather.Queries.GetCitiesList;

namespace FMApp
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
            // Api secret key config
            services.Configure<WeatherApiConfig>(Configuration.GetSection("Weather"));

            // Add external services
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<ILocationService, LocationServiceDummyImpl>();

            // Add MediatR
            services.AddMediatR(typeof(GetCitiesListQuery).GetTypeInfo().Assembly);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=View}/{action=Index}");
            });
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ParkAPI.Data;
using ParkAPI.Repository.BL;
using ParkAPI.Repository.InterfaceClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ParkAPI.AutoMapper;
using System.Reflection;
using System.IO;
using trailAPI.Repository.BL;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using ParkAPI;

namespace ParkApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ParkContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("park")));

            services.AddScoped<INationalPark, ClsNationalPark>();
            services.AddScoped<ITrail, ClsTrail>();
            services.AddVersionedApiExplorer(options => options.GroupNameFormat =" 'v' VVV");

            services.AddAutoMapper(typeof(ParkMapper));
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;

            });
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();//replacing SwaggerDoc
            services.AddSwaggerGen();
            //services.AddSwaggerGen(options=>
            //{
            //    options.SwaggerDoc("ParkOpenApiSpec",
            //        new Microsoft.OpenApi.Models.OpenApiInfo()
            //        {
            //            Title = "Park API",
            //            Version = "1",
            //            Description="Restiful API",
            //            Contact= new Microsoft.OpenApi.Models.OpenApiContact()
            //            {
            //                Email="ahmedmegahed307@gmail.com",
            //                Name="AHMED MEGAHED",
            //                Url=new Uri("https://github.com/ahmedmegahed307")
            //            }
            //        });
            //    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
            //    options.IncludeXmlComments(xmlCommentsFullPath);


            //    options.SwaggerDoc("ParkOpenApiSpecForTrail",
            //       new Microsoft.OpenApi.Models.OpenApiInfo()
            //       {
            //           Title = "Trail API",
            //           Version = "1",
            //           Description = "Trail Restiful API",
            //           Contact = new Microsoft.OpenApi.Models.OpenApiContact()
            //           {
            //               Email = "ahmedmegahed307@gmail.com",
            //               Name = "AHMED MEGAHED",
            //               Url = new Uri("https://github.com/ahmedmegahed307")
            //           }
            //       });

            //});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            //app.UseSwaggerUI(options=>
            //{
            //    options.SwaggerEndpoint("/swagger/ParkOpenApiSpec/swagger.json", "PARK API");
            //    //options.SwaggerEndpoint("/swagger/ParkOpenApiSpecForTrail/swagger.json", "Trails API");
            //    options.RoutePrefix = "";

            //});
            app.UseSwaggerUI(options =>
            {
                foreach (var desc in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{desc.GroupName}/swagger.json", desc.GroupName.ToUpperInvariant());
                options.RoutePrefix = "";
            });
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();
            });
        }
    }
}

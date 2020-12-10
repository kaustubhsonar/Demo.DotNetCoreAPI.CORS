using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Demo.DotNetCoreAPI.CORS
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
            services.AddControllers();
            //To allow all origins 
            //services.AddCors(options => options.AddDefaultPolicy(
            //    builder => builder.AllowAnyOrigin()));

            //To allow selected origins in default policy 
            //services.AddCors(options =>
            //{
            //    options.AddDefaultPolicy(
            //            builder => builder.WithOrigins("http://localhost:5006", "http://localhost:5005"));
            //});

            //To create a custom policy
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder => builder.WithOrigins("http://localhost:5006", "http://localhost:5000"));
                options.AddPolicy("demoPolicy", 
                    builder => builder.WithOrigins("http://localhost:5005"));                
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseCors();
            //app.UseCors("demoPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapControllers().RequireCors("demoPolicy");
            });
        }
    }
}

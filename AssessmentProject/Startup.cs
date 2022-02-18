using AssessmentProject.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.EntityFrameworkCore;
using AssessmentProject.Core.Interfaces;
using AssessmentProject.Service;
using Microsoft.OpenApi.Models;

namespace AssessmentProject
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
            services.AddDbContext<AssessmentContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AssessmentConnection")));
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Contact = new OpenApiContact { Email = "ajibademichael@gmail.com", Name = "Ajibade Michael" },
                    Description = "API Documentation",
                    Title = "API Documentation",
                    Version = "v1"
                });
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                var prefix = string.IsNullOrWhiteSpace(options.RoutePrefix) ? "." : "..";
                options.SwaggerEndpoint($"{prefix}/swagger/v1/swagger.json", "API Documentation");
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

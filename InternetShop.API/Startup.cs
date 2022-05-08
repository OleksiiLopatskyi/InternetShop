﻿using InternetShop.API.Extensions;
using InternetShop.BAL.Options;
using InternetShop.DAL.DataContext;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace InternetShop.API
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
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(i => i.UseSqlServer(connection));

            services.ConfigureSwagger();
            services.ConfigureJWT(Configuration);
            services.ConfigureHttpClient();
            services.Configure<FileStorageOptions>(Configuration
                .GetSection(FileStorageOptions.FileStorageAPI));
            services.Configure<JwtOptions>(Configuration
                .GetSection(JwtOptions.JwtSection));
            services.ConfigureServices();

            services.AddControllers().AddFluentValidation(options =>
            {
                options.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                options.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InternetShop v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();
             app.UseCors(builder => builder.WithOrigins()
             .AllowAnyOrigin()
             .AllowAnyHeader()
             .AllowAnyMethod());

            app.UseAuthentication();   
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FriendZone.API.Data;
using FriendZone.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace FriendZone.API
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
            services.AddDbContext<FriendZoneDbCondext>(op =>
                op.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Adding Cors Origin Policy
            services.AddCors(op =>
            {
                op.AddPolicy("Cors", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });

            services.Configure<Application>(Configuration.GetSection("Application"));

            //Register services
            services.AddScoped<IAuthRepository, AuthRepository>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //Add Authentication Middleware
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("JWT:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            //Add Swagger UI
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info() { Title = "Core API", Description = "Swagger Core API" });
                var xmlPath = System.AppDomain.CurrentDomain.BaseDirectory + "FriendZone.API.xml";
                x.IncludeXmlComments(xmlPath);
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Cors");
            app.UseAuthentication();
            app.UseMvc();

            //Enable Swagger UI
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Core API");
            });
        }
    }
}

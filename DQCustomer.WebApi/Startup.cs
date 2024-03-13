using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DQCustomer.WebApi.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using DQCustomer.Common;

namespace DQCustomer.WebApi
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
            services.Configure<DatabaseConfiguration>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ApiGatewayConfig>(Configuration.GetSection("ApiGateway"));
            //services.Configure<ElasticConfig>(Configuration.GetSection("ElasticServer"));

            // config consul
            services.AddConsulConfig(Configuration);

            // configure for testing token
            ConfigureJWTAuthentication(services);

            //configure swagger
            ConfigureSwaggerDoc(services);

            //MediatR for rabbitMQ
            services.AddMediatR(typeof(Startup));
            //RegisterServices(services);

            //Cache
            services.AddMemoryCache();
        }

        //private void RegisterServices(IServiceCollection services)
        //{
        //    DependencyContainer.RegisterServices(services);
        //}

        private void ConfigureSwaggerDoc(IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
                s.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                s.SwaggerDoc(Configuration["Service:DocName"], new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = Configuration["Service:Title"],
                    Version = Configuration["Service:Version"],
                    Description = Configuration["Service:Description"],
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = Configuration["Service:Contact:Name"],
                        Email = Configuration["Service:Contact:Email"]
                    }
                });

                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, Configuration["Service:XmlFile"]);
                s.IncludeXmlComments(xmlPath);
            });
        }

        private void ConfigureJWTAuthentication(IServiceCollection services)
        {
            var jwtConfig = Configuration.GetSection("JwtToken");
            services.AddCors();
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig["Issuer"] ?? "DefaultIssuer",
                    ValidAudience = jwtConfig["Audience"] ?? "DefaultAudience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["SecretKey"]))
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            //Consul Descovery Service
            app.UseConsul(Configuration);

            //JWT
            app.UseCors(option => option
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

            //JWT
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            // swagger
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint($"/doc/{Configuration["Service:DocName"]}/swagger.json",
                    $"{Configuration["Service:Name"]} {Configuration["Service:Version"]}");
            });

        }
    }
}

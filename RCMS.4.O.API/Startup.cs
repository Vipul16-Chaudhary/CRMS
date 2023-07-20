using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using RCMS._4.O.Common;
using RCMS._4.O.ConfigurationManager;
using RCMS._4.O.Interfaces.CaptchaInterface;
using RCMS._4.O.Interfaces.RabbitMQInterface;
using RCMS._4.O.Interfaces.RadisCacheInterface;
using RCMS._4.O.Interfaces.RCMSInterface;
using RCMS._4.O.Repository.CaptchaRepository;
using RCMS._4.O.Repository.RabbitMQRepository;
using RCMS._4.O.Repository.RadisCacheRepository;
using RCMS._4.O.Repository.RCMSRepository;
using RCMS._4.O.Utilities;
using RCMS._4.O.Utilities.JWTToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCMS._4.O.API
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
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            ConnectionManager.Initialize(Configuration);

            services.AddMvcCore(formatter =>
            {
                formatter.FormatterMappings.SetMediaTypeMappingForFormat(
                      "json", MediaTypeHeaderValue.Parse("application/json"));
                formatter.FormatterMappings.SetMediaTypeMappingForFormat(
                      "xml", MediaTypeHeaderValue.Parse("application/xml"));
                formatter.FormatterMappings.SetMediaTypeMappingForFormat(
                     "html", MediaTypeHeaderValue.Parse("text/html"));
                formatter.FormatterMappings.SetMediaTypeMappingForFormat(
                      "csv", MediaTypeHeaderValue.Parse("text/html"));

            });
            //services.AddSwaggerGen(c =>
            //{
            //    c.DocumentFilter<ApiSchemasVisibility>();
            //});
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            //services.AddScoped<JWTCustomAuthorization>(); // JWT Middleware open once development starts
            services.AddScoped<IRCMSInterface, RCMSRepository>();
            services.AddScoped<IRadisCacheInterface, RadisCacheRepository>();
            services.AddScoped<IRabitMQProducerInterface, RabitMQProducerRepository>();
            services.AddScoped<ICaptchaInterface, CaptchaRepository>();
            //services.AddScoped<JwtTokenGenerationHelper, JwtTokenGenerationHelper>();
            //services.AddScoped<JwtTokenValidiateHelper, JwtTokenValidiateHelper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RCMSAPI", Version = "v1" });
                //c.EnableAnnotations();

            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction()) //Production and development both server
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();

                if (env.IsDevelopment())
                {
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RCMSAPI v1");
                    });
                }
                else
                {
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "RCMSAPI v1");
                        c.RoutePrefix = string.Empty;         //For the production release
                    });
                }
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

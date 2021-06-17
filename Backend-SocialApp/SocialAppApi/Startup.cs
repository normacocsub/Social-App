using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SocialAppApi.Config;
using SocialAppApi.Hubs;
/*
,
  "ConnectionStrings": {
    "DefaultConnection": "Server=DESKTOP-F0HC5CA\\SQLEXPRESS;Database=socialApp;Trusted_Connection = True; MultipleActiveResultSets = true"
  }
*/
namespace SocialAppApi
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

            #region    configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSetting");
            services.Configure<AppSetting>(appSettingsSection);
            #endregion
            
            #region Configure jwt authentication inteprete el token 
            var appSettings = appSettingsSection.Get<AppSetting>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            
            
            
            //contextos base de datos
            services.AddCors(options =>
            {
                    options.AddPolicy("CorsPolicy", builder => builder.WithOrigins("http://localhost:8100")
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .SetIsOriginAllowed((host) => true));
            });
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            #endregion

            services.AddSignalR();
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<SocialAppContext>(Context => Context.UseSqlServer(connectionString));

            
            
            
            //Agregar OpenApi Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "School API",
                    Description = "School API - ASP.NET Core Web API",
                    TermsOfService = new Uri("https://cla.dotnetfoundation.org/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Social App",
                        Email = string.Empty,
                        Url = new Uri("https://cla.dotnetfoundation.org/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licencia dotnet foundation",
                        Url = new Uri("https://www.byasystems.co/license"),
                    }
                });
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                c.OperationFilter<AuthOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SocialAppApi v1"));
            }

            app.UseHttpsRedirection();


            app.UseRouting();
            #region global cors policy activate Authentication/Authorization
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "wwwroot/ArchivosMultimedia")),
                RequestPath = "/ArchivosMultimedia"
            });

            app.UseDirectoryBrowser(new DirectoryBrowserOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, "wwwroot/ArchivosMultimedia")),
                RequestPath = "/ArchivosMultimedia"
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalHub>("/signalHub");

            });

           
        }


    }
}

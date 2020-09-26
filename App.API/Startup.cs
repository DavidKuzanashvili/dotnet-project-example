using App.Domain.Entities.Users;
using App.Infrastructure.Authorization.Configuration;
using App.Infrastructure.Configuration;
using App.Infrastructure.Configuration.Core;
using App.Infrastructure.Configuration.DIServices;
using App.Infrastructure.Configuration.Swagger;
using App.Infrastructure.DataAccess;
using App.Infrastructure.DataAccess.Seeders;
using App.Infrastructure.Utils.Helpers.Uploaders;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace App.API
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
            //services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddMailKit(config =>
                config.UseMailKit(Configuration.GetSection("SMTPSettings").Get<MailKitOptions>()));

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation();

            services.PostConfigure<MvcNewtonsoftJsonOptions>(options =>
            {
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter()
                {
                    DateTimeStyles = DateTimeStyles.AdjustToUniversal
                });
            });

            services.AddCors();
            services.AddSwagger();
            services.AddAuthentication();

            //Auth START
            services.ConfigureIdentity();
            services.ConfigureJwt(Configuration);
            services.ConfigureJwtTokenOptions(Configuration);
            //Auth END

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            Configuration.ConfigureCoreSettings();
            services.AddAppDIServices();
            services.AddUploadDIService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ILoggerFactory loggerFactory, AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            dbContext.SeedUserRoles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(options => options.SetIsOriginAllowed(x => _ = true)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.ConfigureSwagger();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.ConfigureLoggerFactory();
        }
    }
}

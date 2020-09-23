using App.Infrastructure.Authorization.Settings;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace App.Infrastructure.Authorization.Configuration
{
    public static class JWT
    {
        public static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JWTSettings>(opt =>
            {
                opt.Key = configuration["JWTSettings:Key"];
                opt.ExpireMinute = configuration["JWTSettings:ExpireMinute"];
                opt.RefreshKey = configuration["JWTSettings:RefreshKey"];
                opt.RefreshExpireMinute = configuration["JWTSettings:RefreshExpireMinute"];
            });

            return services;
        }

        public static AuthenticationBuilder ConfigureJwtTokenOptions(this IServiceCollection services, IConfiguration Configuration)
        {
            var key = Configuration["JWTSettings:Key"];
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                ClockSkew = TimeSpan.Zero // remove delay of token when expire
            };

            services.AddSingleton(tokenValidationParameters);
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims

            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = tokenValidationParameters;
            });
        }
    }
}

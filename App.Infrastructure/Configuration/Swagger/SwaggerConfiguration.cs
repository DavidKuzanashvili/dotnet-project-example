using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace App.Infrastructure.Configuration.Swagger
{
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            var name = Assembly.GetEntryAssembly().GetName().Name.Split(".")[0];
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = $"{name} API",
                    Version = "v1"
                });

                c.OperationFilter<RequiredHeaderParameter>();

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer"
                });

                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
            });

            return services;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetExecutingAssembly().GetName().Name.Split(".")[0]} V1");
            });

            return app;
        }

        public class RequiredHeaderParameter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "Accept-Language",
                    In = ParameterLocation.Header,
                    Description = "language",
                    Required = false,
                    Schema = new OpenApiSchema
                    {
                        Type = "String",
                        Default = new OpenApiString("en")
                    }
                });
            }
        }

        public class AuthenticationRequirementsOperationFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (operation.Security == null)
                    operation.Security = new List<OpenApiSecurityRequirement>();


                var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    [scheme] = new List<string>()
                });
            }
        }
    }
}

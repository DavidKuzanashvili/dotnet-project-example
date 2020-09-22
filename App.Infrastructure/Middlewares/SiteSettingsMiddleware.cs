using Microsoft.AspNetCore.Builder;

namespace App.Infrastructure.Middlewares
{
    public static class SiteSettingsMiddleware
    {
        public static void UseSiteSettings(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var header = context.Request.Headers;
                var token = header["Authorization"];


                if (token.Count > 0)
                {

                    //var jwtService = context.RequestServices.GetRequiredService<IAuthService>();
                    //var jwtTokenGenerator = context.RequestServices.GetRequiredService<IJWTTokenGenerator>();

                    //var parsedToken = token[0].Replace("Bearer ", "");

                    //if (jwtTokenGenerator.ValidateToken(parsedToken))
                    //{
                    //    var userClaims = await jwtService.GetUserClaimsAsync(parsedToken);

                    //    context.RequestServices.GetService<UserSettings>().UserId = userClaims.UserId;
                    //    context.RequestServices.GetService<UserSettings>().UserName = userClaims.UserName;
                    //    context.RequestServices.GetService<UserSettings>().UserRoles = userClaims.UserRoles;
                    //}
                }

                await next();
            });
        }
    }
}

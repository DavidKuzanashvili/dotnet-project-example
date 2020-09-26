using App.Infrastructure.Utils.StaticContent;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace App.Infrastructure.DataAccess.Seeders
{
    public static class DataSeeder
    {
        public static void SeedUserRoles(this AppDbContext dbContext)
        {
            if (!dbContext.Roles.Any())
            {
                var roles = new List<IdentityRole>
                {
                    GetRoleItem(UserRoles.Admin),
                    GetRoleItem(UserRoles.User)
                };

                dbContext.Roles.AddRange(roles);
                dbContext.SaveChanges();
            }
        }

        private static IdentityRole GetRoleItem(string role)
        {
            return new IdentityRole
            {
                Name = role,
                NormalizedName = role.ToUpper()
            };
        }
    }
}

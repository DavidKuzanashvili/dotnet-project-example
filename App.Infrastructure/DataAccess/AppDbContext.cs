using App.Domain.Entities.Info;
using App.Domain.Entities.Languages;
using App.Domain.Entities.Users;
using App.Infrastructure.DataAccess.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.DataAccess
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {
        }

        public DbSet<Language> Languages { get; set; }
        public DbSet<Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new LanguageEntityConfiguration());
            builder.ApplyConfiguration(new BlogTranslationEntityConfiguration());
        }
    }
}

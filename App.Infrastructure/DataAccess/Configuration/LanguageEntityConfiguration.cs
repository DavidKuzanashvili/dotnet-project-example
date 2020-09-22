using App.Domain.Models.Languages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.DataAccess.Configuration
{
    public class LanguageEntityConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(x => new { x.Code });
            builder.HasIndex(x => x.Code).IsUnique();

            builder.HasData
            (
                new Language
                {
                    Code = "ka",
                    Name = "ქართული"
                },
                new Language
                {
                    Code = "en",
                    Name = "English"
                }
            );
        }
    }
}

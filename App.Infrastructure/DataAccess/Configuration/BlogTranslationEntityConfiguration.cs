using App.Domain.Entities.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Infrastructure.DataAccess.Configuration
{
    public class BlogTranslationEntityConfiguration : IEntityTypeConfiguration<BlogTranslation>
    {
        public void Configure(EntityTypeBuilder<BlogTranslation> builder)
        {
            builder.HasIndex(x => x.Slug)
                .IsUnique();
        }
    }
}

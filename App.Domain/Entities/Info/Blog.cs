using App.Domain.Entities.Languages;
using System.Collections.Generic;

namespace App.Domain.Entities.Info
{
    public class Blog
    {
        public int Id { get; set; }
        public string CoverImageName { get; set; }
        public string ImageName { get; set; }
        public string ThumbnailImageName { get; set; }
        public string VideoUrl { get; set; }
        public List<BlogTranslation> Translations { get; set; }
    }

    public class BlogTranslation : BaseTranslation
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public bool Published { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}

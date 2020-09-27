using App.Domain.Entities.Info;
using App.Domain.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Domain.Models.Info
{
    public class BlogDTO
    {
        public BlogDTO()
        {
            Translations = new List<BlogTranslationDTO>();
        }

        public BlogDTO(Blog model)
        {
            Id = model.Id;
            CreateDate = model.CreateDate;
            CoverImage = model.CoverImageName.GetDisplayUrl("blogs");
            Image = model.ImageName.GetDisplayUrl("blogs");
            ThumbnailImage = model.ThumbnailImageName.GetDisplayUrl("blogs");
            VideoUrl = model.VideoUrl;
            Translations = model.Translations?
                .Select(x => new BlogTranslationDTO(x))
                .ToList();
        }

        public int Id { get; set; }
        public bool Published { get; set; }
        public DateTime CreateDate { get; set; }
        public string CoverImage { get; set; }
        public string Image { get; set; }
        public string ThumbnailImage { get; set; }
        public string VideoUrl { get; set; }
        public List<BlogTranslationDTO> Translations { get; set; }

        public void Transform(out Blog entity)
        {
            entity = new Blog()
            {
                CoverImageName = CoverImage,
                ImageName = Image,
                ThumbnailImageName = ThumbnailImage,
                VideoUrl = VideoUrl,
                Published = Published,
                Translations = Translations?
                    .Select(x => new BlogTranslation()
                    {
                        LanguageCode = x.LanguageCode.ToLower(),
                        Slug = x.Slug,
                        Title = x.Title,
                        Description = x.Description
                    })
                    .ToList()
            };
        }
    }

    public class BlogTranslationDTO
    {
        public BlogTranslationDTO()
        {
        }

        public BlogTranslationDTO(BlogTranslation model)
        {
            LanguageCode = model.LanguageCode;
            Title = model.Title;
            Description = model.Description;
            Slug = model.Slug;
        }

        public string LanguageCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }

    public class BlogTranslatedDTO
    {
        public BlogTranslatedDTO()
        {
        }

        public BlogTranslatedDTO(Blog model, string langCode)
        {
            Id = model.Id;
            CreateDate = model.CreateDate;
            CoverImage = model.CoverImageName.GetDisplayUrl("blogs");
            Image = model.ImageName.GetDisplayUrl("blogs");
            ThumbnailImage = model.ThumbnailImageName.GetDisplayUrl("blogs");
            VideoUrl = model.VideoUrl;

            var translations = model.Translations?
                .FirstOrDefault(x => x.LanguageCode == langCode);
            Title = translations?.Title;
            Description = translations?.Description;
            Slug = translations?.Slug;
        }

        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string CoverImage { get; set; }
        public string Image { get; set; }
        public string ThumbnailImage { get; set; }
        public string VideoUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
    }
}

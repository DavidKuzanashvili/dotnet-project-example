using App.Domain.Entities.Info;
using App.Domain.Interfaces.Info;
using App.Domain.Models.Info;
using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Domain.Models.Shared.Response;
using App.Infrastructure.DataAccess;
using App.Infrastructure.Services.Shared;
using App.Infrastructure.Utils.Common;
using App.Infrastructure.Utils.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.Info
{
    public class BlogService : BaseService, IBlogService
    {
        public BlogService(AppDbContext db)
            : base(db)
        {
        }

        public async Task<StandardResponse<BlogDTO>> CreateAsync(BlogDTO model)
        {
            model.Translations.ForEach(async (item) =>
            {
                var slug = Slugifier.Slugify(item.Title);
                item.Slug = await CheckSlugAsync(slug, (BlogTranslation x) => x.Slug == slug);
            });

            model.Transform(out Blog entity);

            var response = await _db.Blogs.AddAsync(entity);
            await _db.SaveChangesAsync();

            var result = new StandardResponse<BlogDTO>()
            {
                Data = new BlogDTO(response.Entity)
            };
            return result;
        }

        public async Task<StandardResponse<object>> DeleteByIdAsync(int id)
        {
            var response = await _db.Blogs
                .Include(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Id == id);

            CheckNULLReference(response);
            _db.Blogs.Remove(response);
            var result = new StandardResponse<object>
            {
                Data = new { response.Id }
            };

            await _db.SaveChangesAsync();

            return result;
        }

        public async Task<PaginationResponse<BlogDTO>> GetAsync(PagingParameters pagination, BlogQuery q)
        {
            var query = _db.Blogs.AsQueryable();
            var totalRecords = await query.CountAsync();
            Query(ref query, q);
            var filteredRecords = await query.CountAsync();
            query = query.Paginate(pagination);
            var result = new PaginationResponse<BlogDTO>()
            {
                Data = await query.Select(x => new BlogDTO(x)).ToListAsync(),
                TotalRecords = totalRecords,
                FilterdRecords = filteredRecords,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return result;
        }

        public async Task<BlogDTO> GetBySlugAsync(string slug)
        {
            var entity = await _db.Blogs
                .Include(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Translations.Any(x => x.Slug == slug));

            CheckNULLReference(entity);

            var result = new BlogDTO(entity);

            return result;
        }

        public async Task<BlogTranslatedDTO> GetBySlugLocaledAsync(string slug, string langCode = "en")
        {
            var entity = await _db.Blogs
                .Include(x => x.Translations)
                .FirstOrDefaultAsync(x => x.Translations.Any(x => x.LanguageCode == langCode && x.Slug == slug));

            CheckNULLReference(entity);

            var result = new BlogTranslatedDTO(entity, langCode);

            return result;
        }

        public async Task<PaginationResponse<BlogTranslatedDTO>> GetLocaledAsync(PagingParameters pagination,
            BlogQuery q, string langCode = "en")
        {
            var query = _db.Blogs.AsQueryable();
            var totalRecords = await query.CountAsync();
            Query(ref query, q, langCode);
            var filteredRecords = await query.CountAsync();
            query = query.Paginate(pagination);
            var result = new PaginationResponse<BlogTranslatedDTO>()
            {
                Data = await query.Select(x => new BlogTranslatedDTO(x, langCode)).ToListAsync(),
                TotalRecords = totalRecords,
                FilterdRecords = filteredRecords,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return result;
        }

        public async Task<StandardResponse<BlogDTO>> UpdateAsync(BlogDTO model)
        {
            model.Transform(out Blog entity);

            _db.Entry(entity).State = EntityState.Modified;
            await _db.SaveChangesAsync();

            var result = new StandardResponse<BlogDTO>()
            {
                Data = new BlogDTO(entity)
            };

            return result;
        }

        private void Query(ref IQueryable<Blog> query, BlogQuery q, string langCode = "")
        {
            query = query
                .Include(x => x.Translations)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(langCode))
            {
                query = query.Where(x => x.Published);
            }

            if (!string.IsNullOrWhiteSpace(q.QueryString))
            {
                if (!string.IsNullOrWhiteSpace(langCode))
                {
                    query = query.Where(x => x.Translations
                        .Any(x => x.LanguageCode == langCode
                            && (x.Title.ToLower().Contains(q.QueryString)
                            || x.Description.ToLower().Contains(q.QueryString))
                        )
                    );
                }
                else
                {
                    query = query.Where(x => x.Translations
                        .Any(x => x.Title.ToLower().Contains(q.QueryString)
                            || x.Description.ToLower().Contains(q.QueryString)
                        )
                    );
                }

            }

            if (q.From != null)
            {
                query = query.Where(x => x.CreateDate >= q.From);
            }

            if (q.To != null)
            {
                query = query.Where(x => x.CreateDate <= q.To);
            }
        }
    }
}

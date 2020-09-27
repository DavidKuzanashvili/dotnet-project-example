using App.Domain.Models.Info;
using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Domain.Models.Shared.Response;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Info
{
    public interface IBlogService
    {
        Task<PaginationResponse<BlogDTO>> GetAsync(PagingParameters pagination, BlogQuery q);
        Task<PaginationResponse<BlogTranslatedDTO>> GetLocaledAsync(PagingParameters pagination,
            BlogQuery q, string langCode = "en");
        Task<BlogDTO> GetBySlugAsync(string slug);
        Task<BlogTranslatedDTO> GetBySlugLocaledAsync(string slug, string langCode = "en");

        Task<StandardResponse<BlogDTO>> CreateAsync(BlogDTO model);
        Task<StandardResponse<BlogDTO>> UpdateAsync(BlogDTO model);
        Task<StandardResponse<object>> DeleteByIdAsync(int id);
    }
}

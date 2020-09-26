using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Domain.Models.Shared.Response;
using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Users
{
    public interface IUserService
    {
        Task<StandardResponse<IdentityResult>> DeleteByIdAsync(string id);
        Task<UserResponse> GetByIdAsync(string id);
        Task<PaginationResponse<UserResponse>> GetAsync(PagingParameters pagination,
            CommonQuery query, string currentUserId);
    }
}

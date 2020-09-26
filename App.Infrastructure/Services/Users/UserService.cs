using App.Domain.Entities.Users;
using App.Domain.Interfaces.Users;
using App.Domain.Models.Shared.Pagination;
using App.Domain.Models.Shared.Queries;
using App.Domain.Models.Shared.Response;
using App.Domain.Models.Users;
using App.Infrastructure.Services.Shared;
using App.Infrastructure.Utils.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.Users
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManger;

        public UserService(UserManager<User> userManager)
        {
            _userManger = userManager;
        }

        public async Task<StandardResponse<IdentityResult>> DeleteByIdAsync(string id)
        {
            var user = await _userManger.FindByIdAsync(id);
            CheckNULLReference(user);
            var identityResult = await _userManger.DeleteAsync(user);
            var result = new StandardResponse<IdentityResult>
            {
                Data = identityResult
            };

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    result.ErrorResponse.Errors.Add(new ErrorKeyValuePair()
                    {
                        Key = error.Code,
                        Values = new string[] { error.Description }
                    });
                }
            }

            return result;
        }

        public async Task<PaginationResponse<UserResponse>> GetAsync(PagingParameters pagination, CommonQuery q,
            string currentUserId)
        {
            var query = _userManger.Users.Where(x => x.Id != currentUserId);
            int totalRecords = await query.CountAsync();

            if (!string.IsNullOrWhiteSpace(q.QueryString))
            {
                query = query.Where(x => x.UserName.ToLower().Contains(q.QueryString)
                    || x.FirstName.ToLower().Contains(q.QueryString)
                    || x.LastName.ToLower().Contains(q.QueryString)
                    || x.Email.ToLower().Contains(q.QueryString)
                    || x.PhoneNumber.ToLower().Contains(q.QueryString)
                );
            }

            query = q.OrderByDesc
                ? query.OrderByDescending(x => x.CreateDate)
                : query.OrderBy(x => x.CreateDate);

            int filteredRecords = await query.CountAsync();
            query = query.Paginate(pagination);

            var result = new PaginationResponse<UserResponse>()
            {
                Data = await query.Select(x => new UserResponse(x)).ToListAsync(),
                TotalRecords = totalRecords,
                FilterdRecords = filteredRecords,
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return result;
        }

        public async Task<UserResponse> GetByIdAsync(string id)
        {
            var result = await _userManger.FindByIdAsync(id);

            return new UserResponse(result);
        }
    }
}

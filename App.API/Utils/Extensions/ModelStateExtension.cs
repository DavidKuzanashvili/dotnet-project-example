using App.Domain.Models.Shared.Response;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace App.API.Utils.Extensions
{
    public static class ModelStateExtension
    {
        public static object GetErrors(this ModelStateDictionary modelstate)
        {
            var errors = modelstate
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new ErrorKeyValuePair
                {
                    Key = x.Key,
                    Values = x.Value.Errors.Select(t => t.ErrorMessage).ToArray()
                }).ToList();

            var result = new ErrorResponse
            {
                Errors = errors,
            };

            return result;
        }
    }
}

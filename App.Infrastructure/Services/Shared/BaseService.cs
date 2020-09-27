using App.Infrastructure.DataAccess;
using App.Infrastructure.Exceptions.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.Shared
{
    public abstract class BaseService
    {
        protected readonly AppDbContext _db;

        public BaseService()
        {
        }

        public BaseService(AppDbContext db)
        {
            _db = db;
        }

        protected static void CheckNULLReference<T>(T model, string message = "") where T : class
        {
            if (model == null)
            {
                message = string.IsNullOrWhiteSpace(message) ? "Entity.NotFound" : message;
                throw new EntityNotFoundException(message);
            }
        }

        protected async Task<string> CheckSlugAsync<T>(string slug,
            Expression<Func<T, bool>> filter,
            string propName = "Slug")
            where T : class, new()
        {
            var result = await _db.Set<T>()
                .Where(filter)
                .ToListAsync();

            if (result == null) return slug;

            int count = 0;
            foreach (var item in result)
            {

                var word = item.GetType()
                    .GetProperty(propName)
                    .GetValue(item)
                    .ToString();

                var lastWord = word.Split('-')
                    .Last();

                if (word == slug)
                {
                    ++count;
                    continue;
                }

                var succeded = int.TryParse(lastWord, out count);
                if (succeded)
                    ++count;
            }

            if (count != 0)
            {
                slug += "-" + count.ToString();
            }

            return slug;
        }
    }
}

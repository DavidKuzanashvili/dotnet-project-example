using App.Domain.Entities.Languages;
using App.Domain.Interfaces.Languages;
using App.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Infrastructure.Services.Languages
{
    public class LangugeService : ILanguageService
    {
        private readonly AppDbContext _db;

        public LangugeService(AppDbContext db)
        {
            _db = db;
        }

        public List<Language> Get()
        {
            var result = _db.Languages.ToList();

            return result;
        }

        public async Task<List<Language>> GetAsync()
        {
            var result = await _db.Languages.ToListAsync();

            return result;
        }
    }
}

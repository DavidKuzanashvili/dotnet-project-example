using App.Domain.Entities.Languages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Languages
{
    public interface ILanguageService
    {
        Task<List<Language>> GetAsync();
        List<Language> Get();
    }
}

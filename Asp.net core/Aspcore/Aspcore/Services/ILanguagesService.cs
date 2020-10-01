using Aspcore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Services
{
    public interface ILanguagesService
    {
        Task<IList<Language>> GetAll();
        Task<Language> Create(Language language);
        Task<Language> GetById(int id);
        Task<Language> Update(Language language);
        Task<Language> Delete(int id);
    }
}

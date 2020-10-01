using Aspcore.Migrations;
using Aspcore.Models;
using Aspcore.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Services
{
    public class LanguagesService : ILanguagesService
    {
        private DatnekContext context;

        public LanguagesService(DatnekContext context)
        {
            this.context = context;
        }

        public async Task<Language> Create(Language language)
        {
            if (language.IsValid())
            {
                try
                {
                    context.Languages.Add(language);
                    await context.SaveChangesAsync();
                    return language;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            throw new Exception(language.Errors());
        }

        public async Task<Language> Delete(int id)
        {
            Language language;
            try
            {
                language = await context.Languages.FindAsync(id);
                context.Languages.Remove(language);
                await context.SaveChangesAsync();
                return language;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IList<Language>> GetAll() => await context.Languages.ToListAsync();

        public async Task<Language> GetById(int id) => await context.Languages.FindAsync(id);

        public async Task<Language> Update(Language language)
        {
            if (language.IsValid())
            {
                try
                {
                    context.Languages.Update(language);
                    await context.SaveChangesAsync();
                    return language;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            throw new Exception(language.Errors());
        }
    }
}

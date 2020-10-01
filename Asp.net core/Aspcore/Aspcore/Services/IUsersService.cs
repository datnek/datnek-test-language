using Aspcore.Models;
using Aspcore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Services
{
    public interface IUsersService
    {
        void setAppSettings(AppSettings appSettings);
        Task<User> Authenticate(string username, string password);
        Task<IList<User>> GetAll();
        Task<User> Create(User user);
        Task<User> GetById(int id);
        Task<User> Update(User user);
        Task<User> Delete(int id);
    }
}

using Aspcore.Migrations;
using Aspcore.Models;
using Aspcore.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aspcore.Services
{
    public class UsersService : IUsersService
    {
        private DatnekContext context;
        private AppSettings appSettings;

      
        public UsersService( DatnekContext context, IOptions<AppSettings> appSettings)
        {
            this.context = context;
            this.appSettings = appSettings?.Value;
        }

        public void setAppSettings(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }
        public async Task<User> Create(User user)
        {
            if (user.IsValid())
            {
                try
                {
                    user.password = user.password.Hash();
                    if (await context.Users.AnyAsync(f=> f.email == user.email))
                    {
                        throw new Exception("This email is already use");
                    }

                    if (await context.Users.AnyAsync(f => f.username == user.username))
                    {
                        throw new Exception("This username is already use");
                    }

                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    return user;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            throw new Exception(user.Errors());
        }

        public async Task<User> Delete(int id)
        {
            User user = null;
            try
            {
                user = await context.Users.FirstOrDefaultAsync(f => f.id == id);
                context.Users.Remove(user);
                await context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        public async Task<IList<User>> GetAll() => await context.Users.ToListAsync();

        public async Task<User> GetById(int id) => await context.Users.FindAsync(id);

        public async Task<User> Update(User user)
        {
            if (user.IsValid())
            {
                try
                {
                    context.Users.Update(user);
                    await context.SaveChangesAsync();
                    return user;
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message);
                }
            }
            throw new Exception(user.Errors());
        }

            public async Task<User> Authenticate(string username, string password)
            {
                password = password.Hash();

                var user = await context.Users.SingleOrDefaultAsync(x => (x.username == username || x.email == username) 
                            && x.password == password);

                // return null if user not found
                if (user == null)
                    return null;

                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, user.id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddYears(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.token = tokenHandler.WriteToken(token);

                return user.WithoutPassword();
            }
    }
}

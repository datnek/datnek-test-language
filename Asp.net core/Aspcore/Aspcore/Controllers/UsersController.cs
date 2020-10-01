using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspcore.Models;
using Aspcore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aspcore.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme), ApiController, Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }


        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] User model)
        {
            var user = await usersService.Authenticate(model.username, model.password);

            if (user == null)
                return BadRequest(new { message = "username or password is incorrect" });
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await usersService.GetAll();
            return Ok(users);
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Create(User user)
        {
            try
            {
                // if (ModelState.IsValid)
                user =  await usersService.Create(user);
                return Ok(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            try
            {
                // if (ModelState.IsValid)
                user = await usersService.Update(user);
                return Ok(user);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await usersService.Delete(id);
                return Ok(new { message= "deleted"});
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

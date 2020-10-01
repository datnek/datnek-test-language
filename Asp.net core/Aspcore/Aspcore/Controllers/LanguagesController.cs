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
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme),ApiController, Route("api/[controller]")]
    public class LanguagesController : ControllerBase
    {
        private ILanguagesService languagesService;
        
        public LanguagesController(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var languages = await languagesService.GetAll();
            return Ok(languages);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            try
            {
                // if (ModelState.IsValid)
                language = await languagesService.Create(language);
                return Ok(language);
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Language language)
        {
            try
            {
                // if (ModelState.IsValid)
                language = await languagesService.Update(language);
                return Ok(language);
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
                var language = await languagesService.Delete(id);
                return Ok(new { message = "deleted" });
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }
    }
}

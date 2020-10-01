using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aspcore.Controllers;
using Aspcore.Migrations;
using Aspcore.Models;
using Aspcore.Services;
using Aspcore.Tests.Provides;
using Aspcore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspcore.Tests.Controllers
{
    [TestClass]
    public class LanguagesControllerUnitTest
    {
        private LanguagesController languagesController;
        private Language language;


        [TestCleanup]
        public void testClean()
        {

        }


        [TestInitialize]
        public async Task Setup()
        {
            // Arrange
            var datnekContext = await DatnekContextProvider.GetDatabaseContext();
            var languagesService = new LanguagesService(datnekContext);
            var user = await GetUser(datnekContext);
            language = new Language()
            {
                slug = "123",
                title = "en",
                speak = 2,
                read = 3,
                understand = 2,
                userId = user.id,
                user = new User(),
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };
            languagesController = new LanguagesController(languagesService);
        }

        public async Task<User> GetUser(DatnekContext datnekContext)
        {
            var usersService = new UsersService(datnekContext, Options.Create(new AppSettings()));
            usersService.setAppSettings(new AppSettings() { Secret = "datnek-language-adb8-4ce5-aa49-7test2020256" });
            var user = new User()
            {
                slug = "123",
                username = "stratege",
                email = "danick.takam@datnek.be",
                password = "password"
            };
            return await usersService.Create(user);
        }


        [TestMethod]
        public async Task GetAll_Language_count_1_and_status_200()
        {
            var result = (OkObjectResult) await languagesController.Create(language);
            var l = (Language)result.Value;

            //Act
            var result1 = (OkObjectResult) await languagesController.GetAll();
            var count = ((IList<Language>)result1.Value).Count();

            Assert.AreEqual(1, count);
            Assert.AreEqual(200, result.StatusCode);
        }

      


        [TestMethod]
        public async Task Put_Language_with_valid_object_and_status_code_200()
        {
            //Arrange 
            var result = (OkObjectResult) await languagesController.Create(language);
            var l = (Language)result.Value;
            l.title = "fr";
            l.read = 5;

            //Act
            var item = (OkObjectResult) await languagesController.Update(l.id, l);
            var l1 = (Language)item.Value;

            Assert.AreEqual(l.title, l1.title);
            Assert.AreEqual(l.read, l1.read);
            Assert.AreEqual(200, item.StatusCode);
        }



        [TestMethod]
        public async Task Post_Language_with_valid_object()
        {
            //Act
            var result = (OkObjectResult) await languagesController.Create(language);
            var l = (Language)result.Value;

            //Assert
            Assert.AreEqual(language.title, l.title);
            Assert.AreNotEqual(0, l.id);
            Assert.AreEqual(200, result.StatusCode);
        }

        [TestMethod]
        public async Task Delete_Language()
        {

            var result = (OkObjectResult) await languagesController.Create(language);
            var l = (Language)result.Value;

            //Act
            var item = (OkObjectResult) await languagesController.Delete(l.id);

            //Assert
            Assert.AreEqual(200, item.StatusCode);
        }

    }
}

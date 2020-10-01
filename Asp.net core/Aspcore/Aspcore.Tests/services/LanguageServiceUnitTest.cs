using Aspcore.Migrations;
using Aspcore.Models;
using Aspcore.Services;
using Aspcore.Tests.Provides;
using Aspcore.Utils;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspcore.Tests.services
{

    [TestClass]
    public class LanguageServiceUnitTest
    {
        private LanguagesService languagesService;

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
            languagesService = new LanguagesService(datnekContext);
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
        public async Task GetAll_LanguageAsync()
        {
            //Act
            var l = await languagesService.Create(language);
            var count = (await languagesService.GetAll())?.Count();

            //Assert
            Assert.AreEqual(1, count);
        }


        [TestMethod]
        public async Task Create_LanguageAsync()
        {
            //Act
            var l = await languagesService.Create(language);

            //Assert
            Assert.AreNotEqual(0, l.id);
        }

        [TestMethod]
        public async Task Update_LanguageAsync()
        {
            //Act
            var l = await languagesService.Create(language);

            l.title = "fr";
            l.read = 5;

            var l1 = await languagesService.Update(l);

            //Assert
            Assert.AreNotEqual(0, l.id);
            Assert.AreEqual(l.read, l1.read);
            Assert.AreEqual(l.title, l1.title);
        }

        [TestMethod]
        public async Task Delete_LanguageAsync()
        {
            //Act
            var u = await languagesService.Create(language);

            var u1 = await languagesService.Delete(u.id);
            var count = (await languagesService.GetAll())?.Count();

            //Assert
            Assert.AreEqual(0, count);
            Assert.AreNotEqual(0, u1.id);
        }
    }
}
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

namespace Aspcore.Tests.Services
{
    
    [TestClass]
    public class usersServiceUnitTest
    {
        private  UsersService usersService;
        private User user;


        [TestCleanup]
        public void testClean()
        {
            
        }


        [TestInitialize]
        public async Task Setup()
        {
            // Arrange
            var datnekContext = await DatnekContextProvider.GetDatabaseContext();
            usersService = new UsersService(datnekContext, Options.Create(new AppSettings()));
            usersService.setAppSettings(new AppSettings() { Secret = "datnek-language-adb8-4ce5-aa49-7test2020256" });
            user = new User()
            {
                slug = "123",
                username = "stratege",
                email = "danick.takam@datnek.be",
                password = "password"
            };
        }
      
        [TestMethod]
        public async Task Authenticate_with_good_credential()
        {
            //Act
            var u = await usersService.Create(user);
            var u1 = await usersService.Authenticate("stratege", "password");

            //Assert
            Assert.IsNotNull(u1);
            Assert.IsNotNull(u1.token);
            Assert.IsNull(u1.password);
        }

        [TestMethod]
        public async Task Authenticate_with_bad_credential()
        {
            //Act
            var u = await usersService.Create(user);

            var u1 = await usersService.Authenticate("stratege", "password2");

            //Assert
            Assert.IsNull(u1);
        }
        
        [TestMethod]
        public async Task GetAll_userAsync()
        {
            //Act
            var u = await usersService.Create(user);
            var count = (await usersService.GetAll())?.Count();

            //Assert
            Assert.AreEqual(1, count);
        }


        [TestMethod]
        public async Task Create_userAsync()
        {
            //Act
            var u = await usersService.Create(user);

            //Assert
            Assert.AreNotEqual(0, u.id);
        }

        [TestMethod]
        public async Task Update_userAsync()
        {
            //Act
            var u = await usersService.Create(user);

            u.username = "danick";
            u.email = "otis.takam@datnek.be";

           var u1 = await usersService.Update(u);

            //Assert
            Assert.AreNotEqual(0, u.id);
            Assert.AreEqual(u.email, u1.email);
            Assert.AreEqual(u.username, u1.username);
        }

        [TestMethod]
        public async Task Delete_userAsync()
        {
            //Act
            var u = await usersService.Create(user);

            var u1 = await usersService.Delete(u.id);

            //Assert
            Assert.AreNotEqual(0, u1.id);
        }
    }
}

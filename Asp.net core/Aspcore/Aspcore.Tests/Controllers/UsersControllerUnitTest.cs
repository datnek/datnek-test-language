using Aspcore.Controllers;
using Aspcore.Models;
using Aspcore.Services;
using Aspcore.Tests.Provides;
using Aspcore.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Aspcore.Tests.Controllers
{
    [TestClass]
    public class UsersControllerUnitTest
    {
        private UsersController usersController;
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
            var usersService = new UsersService(datnekContext, Options.Create(new AppSettings()));
            usersService.setAppSettings(new AppSettings() { Secret = "datnek-language-adb8-4ce5-aa49-7test2020256" });

            user = new User()
            {
                slug = "123",
                username = "stratege",
                email = "danick.takam@datnek.be",
                password = "password"
            };

            usersController = new UsersController(usersService);
        }
      

        [TestMethod]
        public async Task Authenticate_valid_credendial()
        {
            var result1 = (OkObjectResult) await usersController.Create(user);
            var u = (User)result1.Value;

            //Act 
            var result = (OkObjectResult) await usersController.Authenticate(new User() { username = u.username, password= "password" });

            // Assert
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsNotNull(((User)result.Value).token);
        }

        [TestMethod]
        public async Task Authenticate_invalid_credendialAsync()
        {
            var result = (OkObjectResult) await usersController.Create(user);
            var u = (User)result.Value;

            //Act 
            var result1 = (BadRequestObjectResult)await usersController.Authenticate(new User() { username = "danick", password = "password" });

            // Assert
            Assert.AreEqual(400, result1.StatusCode);
        }

        [TestMethod]
        public async Task GetAll_User_count_1_and_status_200()
        {
            var result = (OkObjectResult) await usersController.Create(user);
            var u = (User)result.Value;

            //Act
            var result1 = (OkObjectResult) await usersController.GetAll();
            var count = ((IList<User>)result1.Value).Count();

            Assert.AreEqual(1, count);
            Assert.AreEqual(200, result.StatusCode);
        }




        [TestMethod]
        public async Task Putt_User_with_valid_object_and_status_code_200()
        {
            //Arrange 
            var result = (OkObjectResult)await usersController.Create(user);
            var u = (User)result.Value;
            u.username = "danick";
            u.email = "otis.takam@datnek.be";

            //Act
            var item = (OkObjectResult) await usersController.Update(u.id,u);
            var u1 = (User)item.Value;

            Assert.AreEqual(u.username, u1.username);
            Assert.AreEqual(u.email, u1.email);
            Assert.AreEqual(200, item.StatusCode);
        }



        [TestMethod]
        public async Task Post_User_with_valid_object()
        {
            //Act
            var result = (OkObjectResult) await usersController.Create(user);
            var u = (User)result.Value;

            //Assert
            Assert.AreEqual(user.username, u.username);
            Assert.AreNotEqual(0, u.id);
            Assert.AreEqual(200, result.StatusCode);
        }


        [TestMethod]
        public async Task Delete_User()
        {
            //Arrange 
            var result = (OkObjectResult) await usersController.Create(user);
            var u = (User)result.Value;

            //Act
            var item = (OkObjectResult) await usersController.Delete(u.id);

            Assert.AreEqual(200, item.StatusCode);
        }

    }
}

using Aspcore.Utils;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspcore.Tests.Services
{
    
    [TestClass]
    public class UserServiceUnitTest
    {
        private UserService userService;


        [TestCleanup]
        public void testClean()
        {
            
        }


        [TestInitialize]
        public void Setup()
        {
            // Arrange
            userService = new UserService(Options.Create(new AppSettings()));
            userService.setAppSettings(new AppSettings() { Secret = "wemanity-adb8-4ce5-aa49-7test2020256" });
        }
      
        [TestMethod]
        public void Authenticate_with_good_credential()
        {
            //Act
            var user = userService.Authenticate("stratege", "password");

            //Assert
            Assert.IsNotNull(user);
            Assert.IsNotNull(user.token);
            Assert.IsNotNull(user.password);
        }

        [TestMethod]
        public void Authenticate_with_bad_credential()
        {
            //Act
            var user = userService.Authenticate("stratege", "password2");

            //Assert
            Assert.IsNull(user);
        }
        
        [TestMethod]
        public void GetAll_count_1()
        {
            //Act
            var count = userService.GetAll().Count();

            //Assert
            Assert.AreEqual(1, count);
        }
    }
}

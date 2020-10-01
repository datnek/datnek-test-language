using Aspcore.Models;
using Aspcore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspcore.Tests.Models
{
    [TestClass]
   public class UserTest
    {

        [TestMethod]
        public  void valid_model()
        {
            //Arrange
            User user = new User()
            {
                id = 0,
                slug = "123",
                username = "stratege",
                email = "danick.takam@datnek.be",
                password = "password",
                createdAt =  DateTime.Now,
                updatedAt =  DateTime.Now,
                languages = new List<Language>()
            };

            //Act
            var isValid =  user.IsValid();

            //Asserts
            Assert.IsTrue(isValid);
        }


        [TestMethod]
        public void invalid_model()
        {
            //Arrange
            User user = new User()
            {
               
            };

            //Act
            var errors = user.GetMembers();
            var isValid = user.IsValid();

            //Asserts
            Assert.IsFalse(isValid);
            Assert.IsTrue(errors.Contains("slug"));
            Assert.IsTrue(errors.Contains("email"));
            Assert.IsTrue(errors.Contains("username"));
            Assert.IsTrue(errors.Contains("password"));

        }
    }
}

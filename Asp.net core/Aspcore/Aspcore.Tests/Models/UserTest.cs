using Aspcore.Models;
using Aspcore.Tests.Utils;
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
                password = "password"
            };

            //Act
            var errors =  user.CheckPropertyValidation();

            //Asserts
            Assert.AreEqual(0, errors.Count());
        }


        [TestMethod]
        public void invalid_model()
        {
            //Arrange
            User user = new User()
            {
                id = 0
            };

            //Act
            var errors = user.CheckPropertyValidation();

            //Asserts
            Assert.AreNotEqual(0, errors.Count());
            var slug = ValidEntity.CheckPropertyValidation(user).ElementAt(0).MemberNames.ElementAt(0);
            Assert.AreEqual("slug", slug);
            var username = ValidEntity.CheckPropertyValidation(user).ElementAt(1).MemberNames.ElementAt(0);
            Assert.AreEqual("username", slug);
            var email = ValidEntity.CheckPropertyValidation(user).ElementAt(2).MemberNames.ElementAt(0);
            Assert.AreEqual("email", slug);
            var password = ValidEntity.CheckPropertyValidation(user).ElementAt(3).MemberNames.ElementAt(0);
            Assert.AreEqual("password", slug);

        }
    }
}

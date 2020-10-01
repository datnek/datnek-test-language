using Aspcore.Models;
using Aspcore.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aspcore.Tests.Models
{
    [TestClass]
    public class LanguageTest
    {

        [TestMethod]
        public void valid_model()
        {
            //Arrange
            Language language = new Language()
            {
                id = 0,
                slug = "123",
                title = "en",
                speak = 2,
                read = 3,
                understand = 2,
                userId = 1,
                user = new User(),
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now
            };

            //Act
            var isValid = language.IsValid();

            //Asserts
            Assert.IsTrue(isValid);
        }


        [TestMethod]
        public void invalid_model()
        {
            //Arrange
            Language language = new Language()
            {
               
            };

            //Act
            var errors = language.GetMembers();
            var isValid = language.IsValid();

            //Asserts
            Assert.IsFalse(isValid);
            Assert.IsTrue(errors.Contains("slug"));
            Assert.IsTrue(errors.Contains("title"));
            /*
            Assert.IsTrue(errors.Contains("speak"));
            Assert.IsTrue(errors.Contains("read"));
            Assert.IsTrue(errors.Contains("understand"));
            Assert.IsTrue(errors.Contains("userId"));
            */
        }
    }
}

using Aspcore.Models;
using Aspcore.Tests.Utils;
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
            };

            //Act
            var errors = language.CheckPropertyValidation();

            //Asserts
            Assert.AreEqual(0, errors.Count());
        }


        [TestMethod]
        public void invalid_model()
        {
            //Arrange
            Language language = new Language()
            {
                id = 0
            };

            //Act
            var errors = language.CheckPropertyValidation();

            //Asserts
            Assert.AreNotEqual(0, errors.Count());
            var slug = ValidEntity.CheckPropertyValidation(language).ElementAt(0).MemberNames.ElementAt(0);
            Assert.AreEqual("slug", slug);
            var title = ValidEntity.CheckPropertyValidation(language).ElementAt(1).MemberNames.ElementAt(0);
            Assert.AreEqual("title", title);
            var speak = ValidEntity.CheckPropertyValidation(language).ElementAt(2).MemberNames.ElementAt(0);
            Assert.AreEqual("speak", speak);
            var read = ValidEntity.CheckPropertyValidation(language).ElementAt(3).MemberNames.ElementAt(0);
            Assert.AreEqual("read", read);
            var understand = ValidEntity.CheckPropertyValidation(language).ElementAt(4).MemberNames.ElementAt(0);
            Assert.AreEqual("understand", understand);
            var userId = ValidEntity.CheckPropertyValidation(language).ElementAt(5).MemberNames.ElementAt(0);
            Assert.AreEqual("userId", userId);
        }
    }
}

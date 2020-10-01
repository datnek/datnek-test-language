using System;
using System.Collections.Generic;
using System.Linq;
using Aspcore.Controllers;
using Aspcore.Models;
using Aspcore.Tests.Provides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Aspcore.Tests.Controllers
{
    [TestClass]
    public class LanguagesControllerUnitTest
    {
        private LanguagesController LanguagesController;
        private Language language;



        [TestCleanup]
        public void testClean()
        {

        }


        [TestInitialize]
        public void Setup()
        {
            
        }

    }
}

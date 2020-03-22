using BBS.Core.Helpers.CardHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests.CardGeneration
{
    [TestClass]
    public class GenerateCvcWorksProperly : Specification
    {
        string actualResult;
        bool isValid;

        protected override void Given()
        {
            actualResult = GenerationHelper.GenerateCvc().ToString();
            isValid = actualResult.Length == 3;
        }

        protected override void When()
        {
            base.When();
        }

        [TestMethod]
        public void Then()
        {
            Assert.IsTrue(isValid);
        }
    }
}

using BBS.Core.Helpers.CardHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests.CardGeneration
{
    [TestClass]
    public class GenerateIdentifierWorksCorrectly : Specification
    {
        string actualResult;
        bool isValid;

        protected override void Given()
        {
            actualResult = GenerationHelper.GenerateIdentifier();
            isValid = actualResult.Length == 4;
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

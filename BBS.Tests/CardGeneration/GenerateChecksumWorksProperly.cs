using BBS.Core.Helpers.CardHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests.CardGeneration
{
    [TestClass]
    public class GenerateChecksumWorksProperly : Specification
    {
        string cardNumber;
        string expectedChecksum;
        string actualChecksum;
        protected override void Given()
        {
            cardNumber = "521746983254187";
            expectedChecksum = "3";
            actualChecksum = GenerationHelper.GenerateChecksum(cardNumber);
        }

        protected override void When()
        {
            base.When();
        }

        [TestMethod]
        public void Then()
        {
            Assert.AreEqual(expectedChecksum, actualChecksum);
        }
    }
}

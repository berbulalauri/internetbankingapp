using BBS.Core.Helpers.AccountHelper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests.AccountGeneration
{
    [TestClass]
    public class GetAccountNameWorksProperly : Specification
    {
        string firstInput = "Vitaliy";
        string secondInput = "Gigexs";
        string firstExpectedOutput;
        string secondExpectedOutput;
        bool isValid;

        protected override void Given()
        {
            firstInput = "Vitaliy";
            secondInput = "Gigexs";
            firstExpectedOutput = "Vitaliy's";
            secondExpectedOutput = "Gigexs'";
            isValid = GenerationHelper.GetAccountName(firstInput) == firstExpectedOutput && GenerationHelper.GetAccountName(secondInput) == secondExpectedOutput;
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

using BBS.Core.Helpers.AccountHelper;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Tests.AccountGeneration
{
    [TestClass]
    public class GenerateAccountIdentifierNumberWorksProperly : Specification
    {

        string output;
        bool isValid;
        protected override void Given()
        {
            output = GenerationHelper.GenerateAccountIdentifierNumber();
            isValid = int.TryParse(output, out int result) && result > 10000 && result < 100000;
        }

        protected override void When()
        {
        }

        [TestMethod]
        public void Then()
        {
            Assert.IsTrue(isValid);
        }
        
    }
}

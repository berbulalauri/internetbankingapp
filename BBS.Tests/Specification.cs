using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests
{
    [TestClass]
    public abstract class Specification
    {
        [TestInitialize]
        public void Setup()
        {
            InitAppSettings();
            Given();
            When();
        }
        protected virtual void Given()
        {

        }
        protected virtual void When()
        {

        }
        private void InitAppSettings()
        {

        }
    }
}

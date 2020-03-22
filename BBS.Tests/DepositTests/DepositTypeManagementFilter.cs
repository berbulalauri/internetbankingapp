using BBS.Core.Services;
using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Tests.DepositTests
{
    [TestClass]
    public class DepositTypeManagementFilter : Specification
    {
        Mock<IDepositTypeRepository> depositType;
        List<DepositType> depositTypes;
        List<DepositType> expectedDepositTypes;
        List<DepositType> expectedDepositTypes1;
        List<DepositType> acctualDepositTypes;
        List<DepositType> acctualDepositTypes1;
        IDepositService service;
        string inputColumnName;
        string inputSearchstring;
        string sortingString;
        string inputColumnName1;
        string inputSearchstring1;
        string sortingString1;

        protected override void Given()
        {
            depositType = new Mock<IDepositTypeRepository>();
            depositTypes = new List<DepositType>()
            {
                new DepositType{ Id = 2, Name = "Annual Deposit", AnnualRate = 3, BenefitsDescription = "Big Profit", BonusRate = 1, CurrencyId = 1,
                DepositTermId =2, InterestPaymentId = 3, IsDeleted = false, MaximumDepositAmount = 10000, MinimumDepositAmount = 10, MinimumReplenishmentAmount = 100,
                    Description = "goood to take big profit"},
                new DepositType{ Id = 3, Name = "Childish Deposit", AnnualRate = 4, BenefitsDescription = "Low Profit", BonusRate = 1, CurrencyId = 2,
                DepositTermId =2, InterestPaymentId = 3, IsDeleted = false, MaximumDepositAmount = 20000, MinimumDepositAmount = 20, MinimumReplenishmentAmount = 200,
                    Description = "to take small profit"},
                new DepositType{ Id = 4, Name = "Demand Deposit", AnnualRate = 5, BenefitsDescription = "Profit", BonusRate = 2, CurrencyId = 3,
                DepositTermId =2, InterestPaymentId = 3, IsDeleted = false, MaximumDepositAmount = 30000, MinimumDepositAmount = 5, MinimumReplenishmentAmount = 50,
                    Description = "profit"},
            };

            inputColumnName = "Name";
            inputSearchstring = "child";
            sortingString = "Name";

            inputColumnName1 = "AnnualRate";
            inputSearchstring1 = "5";
            sortingString1 = "AnnualRate";

            expectedDepositTypes = new List<DepositType>()
            {
                new DepositType{ Id = 3, Name = "Childish Deposit", AnnualRate = 4, BenefitsDescription = "Low Profit", BonusRate = 1, CurrencyId = 2,
                DepositTermId =2, InterestPaymentId = 3, IsDeleted = false, MaximumDepositAmount = 20000, MinimumDepositAmount = 20, MinimumReplenishmentAmount = 200,
                    Description = "to take small profit"}
            };

            expectedDepositTypes1 = new List<DepositType>()
            {
                new DepositType{ Id = 4, Name = "Demand Deposit", AnnualRate = 5, BenefitsDescription = "Profit", BonusRate = 2, CurrencyId = 3,
                DepositTermId =2, InterestPaymentId = 3, IsDeleted = false, MaximumDepositAmount = 30000, MinimumDepositAmount = 5, MinimumReplenishmentAmount = 50,
                    Description = "profit"}
            };
        }
        protected override async void When()
        {
            depositType.Setup(x => x.GetAllWithCurrency()).ReturnsAsync(depositTypes);
            service = new DepositService(depositType.Object, null, null, null, null, null, null, null, null);

            depositType.Setup(x => x.GetAllWithCurrency()).ReturnsAsync(depositTypes);
            acctualDepositTypes = await service.Filter(sortingString, inputColumnName, inputSearchstring);

            depositType.Setup(x => x.GetAllWithCurrency()).ReturnsAsync(depositTypes);
            acctualDepositTypes1 = await service.Filter(sortingString1, inputColumnName1, inputSearchstring1);
        }

        [TestMethod]
        public void Then()
        {
            for (int a = 0; a < acctualDepositTypes.Count; a++)
            {
                Assert.AreEqual(expectedDepositTypes[a].Id, acctualDepositTypes[a].Id);
            }

            for (int a = 0; a < acctualDepositTypes1.Count; a++)
            {
                Assert.AreEqual(expectedDepositTypes1[a].Id, acctualDepositTypes1[a].Id);
            }
        }
    }
}

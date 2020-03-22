using BBS.Core.Services;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Models.CustomExceptions;
using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BBS.Tests.ServiceTests.GeneralTransferTests
{
    [TestClass]
    public class ShouldThrowCardOwnerException : Specification
    {
		Mock<IUnitOfWork> unitOfWork;
		Mock<ILogger> inputLogger;
		Mock<IServiceService> inputService;
		Mock<UserManager<User>> userManager;
		Transactions inputTransaction;
		ClaimsPrincipal inputUser;
		Exception expectedResult;
		Exception actualResult;

		protected override void Given()
		{
			inputService = new Mock<IServiceService>();
			unitOfWork = new Mock<IUnitOfWork>();
			inputLogger = new Mock<ILogger>();
			userManager = TestHelpers.GetMockUserManager();
			inputUser = new ClaimsPrincipal();
			inputTransaction = new Transactions { SenderAccountId = 1, ServiceId = 1, Amount = 1 };
			expectedResult = new CardOwnerException();
		}

		protected async override void When()
		{
			unitOfWork.Setup(x => x.CardRepository.GetCardIncludesAsync(It.IsAny<int>())).ReturnsAsync(new Card { Id = 1, UserId = 1 });
			userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new User { Id = 2 });

			IPaymentService service = new PaymentService(unitOfWork.Object, userManager.Object, inputLogger.Object, inputService.Object);

			try
			{
				await service.GeneralTransfer(inputTransaction, inputUser);
			}
			catch (Exception e)
			{
				actualResult = e;
			}
		}

		[TestMethod]
		public void Then()
		{
			Assert.AreEqual(expectedResult.Message, actualResult.Message);
		}
	}
}

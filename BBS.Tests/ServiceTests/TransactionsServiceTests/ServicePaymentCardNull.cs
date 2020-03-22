using BBS.Core.Services;
using BBS.Models.Models;
using BBS.DAL.Repositories.UnitOfWork;
using BBS.Interfaces;
using BBS.Interfaces.Services;
using BBS.Logger;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using BBS.Models.CustomExceptions;

namespace BBS.Tests.ServiceTests.TransactionsServiceTests
{
	[TestClass]
	public class ServicePaymentCardNull : Specification
	{
		Mock<IUnitOfWork> unitOfWork;
		Mock<UserManager<User>> userManager;
		Exception expected;
		Exception result;
		Transactions inputTransaction;
		ClaimsPrincipal inputUser;
		Mock<ILogger> inputLogger;

		protected override void Given()
		{
			expected = new ObjectNotFoundException();
			inputUser = new ClaimsPrincipal();
			inputLogger = new Mock<ILogger>();
			inputTransaction = new Transactions { Service = new Service() { Id = 1 } };
			unitOfWork = new Mock<IUnitOfWork>();
			userManager = TestHelpers.GetMockUserManager();
		}

		

		protected async override void When()
		{
			unitOfWork.Setup(x => x.CardRepository.GetCardIncludesAsync(It.IsAny<int>())).ReturnsAsync((Card)null);
			userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync((User)null);

			ITransactionsService service = new TransactionsService(unitOfWork.Object, userManager.Object, inputLogger.Object);

			try
			{
				await service.ServicePayment(inputTransaction, inputUser);
			}
			catch (Exception e)
			{
				result = e;
			}
		}

		[TestMethod]
		public void Then()
		{
			Assert.AreEqual(expected.Message, result.Message);
		}
	}
}

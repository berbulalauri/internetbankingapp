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

namespace BBS.Tests.ServiceTests.TransactionsServiceTests
{
	[TestClass]
	public class ServicePaymentInvalidAmount : Specification
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
			expected = new InvalidOperationException();
			inputUser = new ClaimsPrincipal();
			inputLogger = new Mock<ILogger>();
			inputTransaction = new Transactions { Service = new Service() { Id = 1, FixedFee=2.0m}, Amount=1m };
			unitOfWork = new Mock<IUnitOfWork>();
			userManager = TestHelpers.GetMockUserManager();
		}

		protected async override void When()
		{
			unitOfWork.Setup(x => x.CardRepository.GetCardIncludesAsync(It.IsAny<int>())).ReturnsAsync(new Card { Id = 1, UserId = 2 });
			userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(new User { Id = 2 });


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

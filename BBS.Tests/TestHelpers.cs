using BBS.Models.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Tests
{

    public class TestHelpers
    {
		static public Mock<UserManager<User>> GetMockUserManager()
		{
			var userStoreMock = new Mock<IUserStore<User>>();
			return new Mock<UserManager<User>>(
				userStoreMock.Object, null, null, null, null, null, null, null, null);
		}
	}
}

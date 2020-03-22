using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
	public interface IMailService
	{
		Task SignupWelcome(string email, string name);
		Task LoanRequest(string email, string name, bool accpeted);
		Task CardRequest(string email, string name, bool accpeted);
		Task DeqositAccept(string email, string name);
		Task PasswordChange(string email, string name);
	}
}

using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
	public interface ITransactionsService
	{
		public Task<List<Transactions>> TransactionsByUserId(int id);
		public void Add(Transactions transactions);
		public Task<Transactions> ServicePayment(Transactions transaction, ClaimsPrincipal currentUser);
	}
}

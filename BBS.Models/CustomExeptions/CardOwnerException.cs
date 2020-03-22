using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.CustomExceptions
{
	public class CardOwnerException : TransactionException
	{
		public override string Message => "You do not have permission to use this card";
	}
}

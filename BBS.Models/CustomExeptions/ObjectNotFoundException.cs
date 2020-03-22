using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.CustomExceptions
{
	public class ObjectNotFoundException : TransactionException
	{
		public override string Message => "Object with specified id was nat found";
	}
}

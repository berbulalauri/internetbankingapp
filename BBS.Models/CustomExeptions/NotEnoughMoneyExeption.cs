using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.CustomExceptions
{
    public class NotEnoughMoneyException:TransactionException
    {
        public override string Message => "Not enough money on your balance!";
    }
}

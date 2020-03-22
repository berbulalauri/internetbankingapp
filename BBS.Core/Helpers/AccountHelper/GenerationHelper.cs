using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Core.Helpers.AccountHelper
{
    public class GenerationHelper
    {
        public static string GenerateAccountIdentifierNumber()
        {
            Random random = new Random();
            return random.Next(10000, 100000).ToString();
        }

        public static string GetAccountName(string firstName)
        {
            return firstName.EndsWith('s') ? firstName + "'" : firstName + "'s";
        }
    }
}

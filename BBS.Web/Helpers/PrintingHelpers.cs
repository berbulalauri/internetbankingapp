using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Helpers
{
    public class PrintingHelpers
    {
        public static string DisplayMoneyWithCurrency(decimal money, string currency)
        {
            return money + " " + currency;
        }

        public static string DisplayTerm(int termInMonth)
        {
            return (termInMonth == 1) 
                ? (termInMonth + " month") 
                : (termInMonth + " months");
        }

        public static string DisplayDate(DateTime date)
        {
            return date.ToShortDateString();
        }
    }
}

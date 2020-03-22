using System;

namespace BBS.Web.Areas.Administration.Helpers
{
    public class FormatingHelper
    {
        public static string FormatDate(DateTime datetime)
        {
            return datetime.ToString("dd.MM.yyyy");
        }

        public static string FormatName(string firstName, string lastName)
        {
            return firstName + " " + lastName;
        }

        public static string FormatRate(decimal rate)
        {
            return rate + " %";
        }

        public static string FormatLoanSum(decimal loanSum, string currencyName)
        {
            return loanSum + " " + currencyName;
        }
    }
}

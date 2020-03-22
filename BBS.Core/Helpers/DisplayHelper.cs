namespace BBS.Core.Helpers
{
    public class DisplayHelper
    {
        public static string DisplayLoanAmountInDescription(decimal amount)
        {
            return $"Transfer money ({amount}) to user's account";
        }
    }
}

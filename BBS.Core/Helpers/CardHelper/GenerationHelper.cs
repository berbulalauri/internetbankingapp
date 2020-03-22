using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Core.Helpers.CardHelper
{
    public class GenerationHelper
    {
        public static string GenerateIdentifier()
        {
            Random random = new Random();
            return random.Next(Constants.CARD_IDENTIFIER_LOWER_LIMIT, Constants.CARD_IDENTIFIER_UPPER_LIMIT).ToString();
        }

        public static int GenerateCvc()
        {
            Random random = new Random();
            return random.Next(Constants.CVC_LOWER_LIMIT, Constants.CVC_UPPER_LIMIT);
        }

        public static string GenerateChecksum(string cardNumber)
        {
            bool isAltered = true;
            int sum = 0;
            for (int i = cardNumber.Length-1; i >= 0; i--)
            {
                if (isAltered)
                {
                    sum += int.Parse(cardNumber[i].ToString()) * 2;

                }
                else
                {
                    sum += int.Parse(cardNumber[i].ToString());
                }
                isAltered = !isAltered;
            }

            int checksum = 0;
            for (int i = 0; i < 10; i++)
            {
                if ((sum + i) % 10 == 0)
                    checksum = i;
            }

            return checksum.ToString();
        }
    }
}

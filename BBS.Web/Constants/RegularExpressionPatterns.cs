using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Constants
{
    public class RegularExpressionPatterns
    {
        public const string OnlyEightNumbers = "^[0 - 9]{8}$";
        public const string OnlyNumbers = "([0-9][0-9]*[/.]?[0-9]*)";
      
    }
}

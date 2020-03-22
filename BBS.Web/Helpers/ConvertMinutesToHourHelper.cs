using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Helpers
{
    public class ConvertMinutesToHourHelper
    {
        public static TimeSpan MinutesToHour(int timeSpan)
        {
            var totalHour = TimeSpan.FromMinutes(timeSpan);
            return totalHour;
        }
    }
}

using BBS.Interfaces.CronJobs.Configurations;
using System;

namespace BBS.Web.Extensions.Configurations.Concrete
{
    public class ScheduleConfig<T> : IScheduleConfig<T>
    {
        public string CronExpression { get; set; }
        public TimeZoneInfo TimeZoneInfo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.Models
{
    public class TravelTime
    {
        public long Ticks { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public int Milliseconds { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public double TotalDays { get; set; }
        public double TotalHours { get; set; }
        public int TotalMilliseconds { get; set; }
        public int TotalMinutes { get; set; }
        public int TotalSeconds { get; set; }
    }
}

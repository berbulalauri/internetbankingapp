using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
    public class UpdateRequestViewModel
    {
        public CardRequest CardRequest { get; set; }
        public City City { get; set; }
        public CardRequestHistory CardRequestHistory { get; set; }
        public List<CardRequestHistory> CardRequestHistories { get; set; }
    }
}

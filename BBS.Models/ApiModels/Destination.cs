using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.Models.ApiModels
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public List<Trip> TripsFromHere { get; set; }
        public List<Trip> TripsToHere { get; set; }
    }
}

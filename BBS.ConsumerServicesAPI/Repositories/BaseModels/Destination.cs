using BBS.ConsumerServicesAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.BaseModels
{
	public class Destination : ISoftDeletable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public bool IsDeleted { get; set; }

		public List<Trip> TripsFromHere { get; set; }
		public List<Trip> TripsToHere { get; set; }
	}
}

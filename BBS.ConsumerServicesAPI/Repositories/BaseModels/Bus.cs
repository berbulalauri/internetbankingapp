using BBS.ConsumerServicesAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.BaseModels
{
	public class Bus : ISoftDeletable
	{
		public int Id { get; set; }
		public int SeatsCount { get; set; }
		public string Model { get; set; }
		public bool IsDeleted { get; set; }

		public List<Trip> Trips { get; set; }
	}
}

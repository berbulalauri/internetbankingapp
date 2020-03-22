using BBS.ConsumerServicesAPI.Repositories;
using BBS.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.BaseModels
{
	public class Trip : ISoftDeletable
	{
		public int Id { get; set; }
		public DateTime Departure { get; set; }
		public int DestinationToId { get; set; }
		public int DestinationFromId { get; set; }
		public int FreeSeatCount { get; set; }
		[Required]
		public decimal TicketPrice { get; set; }
		public int BusId { get; set; }
		public int TravelTime { get; set; }
		public bool IsDeleted { get; set; }

		[JsonIgnore]
		public Destination DestinationTo { get; set; }
		[JsonIgnore]
		public Destination DestinationFrom { get; set; }
		public Bus Bus { get; set; }
		public List<Ticket> Tickets { get; set; }
	}
}

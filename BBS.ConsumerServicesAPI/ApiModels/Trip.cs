using BBS.Models.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.ApiModels
{
	public class Trip
	{
		public int Id { get; set; }
		[AfterCurrentDate]
		[Required]
		public DateTime Departure { get; set; }
		public int FreeSeatCount { get; set; }
		public string DestinationTo { get; set; }
		public string DestinationFrom { get; set; }
		[MoreThanZero]
		[Required]
		public decimal TicketPrice { get; set; }
		public string Bus { get; set; }
		[Display(Name = "Destination To")]
		[Required]
		public int DestinationToId { get; set; }
		[AreNotEqual("DestinationToId")]
		[Display(Name = "Destination From")]
		[Required]
		public int DestinationFromId { get; set; }
		[Required]
		public int BusId { get; set; }
		public int TotalMinutes { get; set; }
		public int TotalSeats { get; set; }
		public List<int> FreeSeats { get; set; }
	}
}

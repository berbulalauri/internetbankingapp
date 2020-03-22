using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Models.ApiModels
{
	public class Trip
	{
		public int Id { get; set; }
		public DateTime Departure { get; set; }
        [Display(Name = "Sits")]
		public int FreeSeatCount { get; set; }
		public decimal TicketPrice { get; set; }
		public string DestinationTo { get; set; }
		public string DestinationFrom { get; set; }
		public string Bus { get; set; }
		public int DestinationToId { get; set; }
		public int DestinationFromId { get; set; }
		public int BusId { get; set; }
		public int TotalSeats { get; set; }						
		public int TotalMinutes { get; set; }
		public List<int> FreeSeats { get; set; }
	}
}

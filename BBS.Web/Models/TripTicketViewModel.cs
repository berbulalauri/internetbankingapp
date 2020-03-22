using BBS.Models.ApiModels;
using BBS.Models.Constants;
using BBS.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Web.Models
{
	public class TripTicketViewModel
	{

		public DateTime Departure { get; set; }
		[DisplayName("Seats")]
		public int FreeSeatCount { get; set; }
		public decimal TicketPrice { get; set; }
		public string DestinationTo { get; set; }
		public string DestinationFrom { get; set; }
		public int TotalMinutes { get; set; }
		public int TotalSeats { get; set; }
		public List<int> FreeSeats { get; set; }
		public string SeatNumbers { get; set; }
		public int SeatNumber { get; set; }
		public string PassangerName{ get; set; }
		public DateTime PurchaseDate { get; set; }
		public decimal Balance { get; set; }
		public decimal TicketTotalPrice { get; set; }
		public DateTime Date { get; set; }

		[Display(Name = "Personal Account")]
		[RegularExpression(RegexConstants.RegexForPersonalAccountId,
				   ErrorMessage = ErrorMessages.InvalidPersonalAccountId)]
		public string PersonalAccountId { get; set; }
		public int ServiceId { get; set; }
		public int ServiceTypeId { get; set; }
		public string ServiceName { get; set; }



		public int TripId { get; set; }
		public Trip Trip { get; set; }

		public int CardId { get; set; }
		public Card Card { get; set; }

		public int AccountId { get; set; }
		public Account Account { get; set; }

		public int SenderCardId { get; set; }
		public int ReceiverAccountId { get; set; }
		public string ServiceProviderName { get; set; }
		public decimal Amount { get; set; }
	}
}

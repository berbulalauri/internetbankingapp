using BBS.ConsumerServicesAPI.Helpers.Constants;
using BBS.ConsumerServicesAPI.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Repositories.BaseModels
{
	public class Ticket : ISoftDeletable
	{
		public int Id { get; set; }

		[MaxLength(100, ErrorMessage = ErrorMessages.PassangerNameMaxLengthError)]
		[MinLength(4 , ErrorMessage = ErrorMessages.PassangerNameMinLengthError)]
		[RegularExpression(RegularExpressions.RegExForPassangerName, ErrorMessage = ErrorMessages.IncorrectPassangerName)]
		public string PassangerName { get; set; }
		public DateTime PurchaseDate { get; set; }


		public int SeatNumber { get; set; }
		public int TripId { get; set; }

		[JsonIgnore]
		public Trip Trip { get; set; }
		public bool IsDeleted { get; set; }
	}
}

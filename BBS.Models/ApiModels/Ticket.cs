using BBS.Models.Constants;
using BBS.Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BBS.Models.ApiModels
{
    public class Ticket
    {
        public int Id { get; set; }

		[MaxLength(100, ErrorMessage = ErrorMessages.PassangerNameMaxLengthError)]
		[MinLength(4, ErrorMessage = ErrorMessages.PassangerNameMinLengthError)]
		[RegularExpression(RegexConstants.RegExForPassangerName, ErrorMessage = ErrorMessages.IncorrectPassangerName)]
		[DisplayName("Passanger Name")]
		public string PassangerName { get; set; }
		public DateTime PurchaseDate { get; set; }

		public int SeatNumber { get; set; }
		public int TripId { get; set; }

		[JsonIgnore]
		public Trip Trip { get; set; }
		public bool IsDeleted { get; set; }
	}
}

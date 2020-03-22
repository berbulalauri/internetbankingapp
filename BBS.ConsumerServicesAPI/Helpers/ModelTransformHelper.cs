
using BBS.ConsumerServicesAPI.Constants;
using System;
using System.Linq;
using Api = BBS.ConsumerServicesAPI.ApiModels;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;

namespace BBS.ConsumerServicesAPI.Helpers
{
	public class ModelTransformHelper
	{
		public static Api.Trip TripDbToApi(Db.Trip trip)
		{
			return new Api.Trip
			{
				Id = trip.Id,
				Departure = trip.Departure,
				DestinationFrom = trip.DestinationFrom?.Name,
				DestinationFromId = trip.DestinationFromId,
				DestinationTo = trip.DestinationTo?.Name,
				DestinationToId = trip.DestinationToId,
				FreeSeats = Enumerable.Range(1, trip.Bus?.SeatsCount??BusService.BusSeatCount).ToList(),
				TotalMinutes = trip.TravelTime,
				Bus = trip.Bus?.Model,
				TotalSeats = trip.Bus?.SeatsCount??50,
				BusId = trip.BusId,
				TicketPrice = trip.TicketPrice,
				FreeSeatCount = trip.FreeSeatCount,
			};
		}

		public static Db.Trip ApiToTripDb(Api.Trip trip)
		{
			return new Db.Trip
			{
				Id = trip.Id,
				FreeSeatCount = trip.FreeSeatCount,
				Departure = trip.Departure,
				DestinationFromId = trip.DestinationFromId,
				DestinationToId = trip.DestinationToId,
				TravelTime = trip.TotalMinutes,
				BusId = trip.BusId,
				TicketPrice = trip.TicketPrice
			};
		}
	}
}

using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using Api = BBS.ConsumerServicesAPI.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BBS.ConsumerServicesAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Db = BBS.ConsumerServicesAPI.Repositories.BaseModels;

namespace BBS.ConsumerServicesAPI.Services
{
	public class TripService : ITripService
	{
		private readonly ITripRepository _tripRepo;
		private readonly ITicketRepository _ticketRepo;

		public TripService(ITripRepository trip, ITicketRepository ticket)
		{
			_tripRepo = trip;
			_ticketRepo = ticket;
		}

		public async Task<ActionResult<Db.Trip>> Create(Api.Trip trip)
		{
			return await _tripRepo.Create(trip);
		}

		public async Task<List<Api.Trip>> GetAllFiltered(string from, string to, DateTime? departure)
		{
			var trips = await _tripRepo.GetAllIncludes();
			List<Repositories.BaseModels.Trip> filtered = trips.Where(x =>
				x.DestinationFrom.Name.Contains(from ?? "", StringComparison.OrdinalIgnoreCase))
				.Where(x => x.DestinationTo.Name.Contains(to ?? "", StringComparison.OrdinalIgnoreCase)).ToList();

			if (departure != null)
			{
				return filtered.Where(t => t.Departure.Date == departure).ToList().ConvertAll(x => ModelTransformHelper.TripDbToApi(x));
			}

			return filtered.ConvertAll(x => ModelTransformHelper.TripDbToApi(x));
		}

		public async Task<Api.Trip> GetTripWithSeats(int id)
		{
			Repositories.BaseModels.Trip trip = await _tripRepo.GetOneIncludes(id);
			List<Ticket> tickets = await _ticketRepo.FindAllAsync(t => t.TripId == id);

			Api.Trip tripRet = ModelTransformHelper.TripDbToApi(trip);
			tripRet.FreeSeatCount = trip.Bus.SeatsCount - tickets.Count;

			foreach (var item in tickets)
			{
				tripRet.FreeSeats.Remove(item.SeatNumber);
			}

			return tripRet;
		}
	}
}

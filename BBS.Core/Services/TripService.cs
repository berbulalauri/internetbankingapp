using BBS.DAL.Clients.Abstract;
using BBS.DAL.Repositories;
using BBS.Interfaces.Services;
using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class TripService : ITripService
    {
        private ITripClient _tripClient;

        public TripService(ITripClient tripClient)
        {
            _tripClient = tripClient;
        }

        public async Task<IEnumerable<Trip>> FilterTrips(int departureId, int destinationId, DateTime date, int sitsCount)
        {

            IEnumerable<Trip> unfilteredTrips = await _tripClient.GetAllAsync();
            List<Trip> filteredTrips = unfilteredTrips.Where(t => t.DestinationFromId == departureId && t.DestinationToId == destinationId && t.Departure.ToShortDateString() == date.ToShortDateString() && t.FreeSeatCount >= sitsCount).ToList();

            return filteredTrips;
        }

        public async Task<Trip> GetTripById(int id)
        {
            return await _tripClient.GetById(id);
        }
    }
}

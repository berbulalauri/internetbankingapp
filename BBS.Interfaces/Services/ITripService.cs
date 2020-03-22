using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Interfaces.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> FilterTrips(int departureId, int destinationId, DateTime date, int sitsCount);
        Task<Trip> GetTripById(int id);
    }
}

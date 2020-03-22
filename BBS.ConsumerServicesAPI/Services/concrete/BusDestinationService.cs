using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using BBS.ConsumerServicesAPI.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services.concrete
{
    public class BusDestinationService : IBusDestinationService
    {
        private readonly IBusDestinationRepository _context;
        public BusDestinationService(IBusDestinationRepository context)
        {
            _context = context;
        }

        public async Task<Destination> DeleteBusDestination(int id)
        {
            return await _context.DeleteBusDestination(id);
        }

        public async Task<List<Destination>> GetAllBusDestinations()
        {
            return await _context.GetAllBusDestinations();
        }

        public async Task<Destination> GetBusDestination(int id)
        {
            return await _context.GetByIdAsync(id);
        }

        public async Task<Destination> UpdateBusDestination(Destination destination)
        {
            return await _context.UpdateBusDestination(destination);
        }
        public async Task<Destination> AddBusDestination(Destination destination)
        {
            return await _context.AddBusDestinationRepo(destination);
        }
    }
}

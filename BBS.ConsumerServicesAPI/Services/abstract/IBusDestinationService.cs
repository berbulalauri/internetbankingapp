using BBS.ConsumerServicesAPI.Repositories.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BBS.ConsumerServicesAPI.Services
{
    public interface IBusDestinationService
    {
        Task<List<Destination>> GetAllBusDestinations();
        Task<Destination> DeleteBusDestination(int id);
        Task<Destination> UpdateBusDestination(Destination destination);
        Task<Destination> AddBusDestination(Destination destination);
        Task<Destination> GetBusDestination(int id);
    }
}

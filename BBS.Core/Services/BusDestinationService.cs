using BBS.DAL.Clients.Abstract;
using BBS.DAL.Repositories.abstractions;
using BBS.Interfaces.Services;
using BBS.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BBS.Core.Services
{
    public class BusDestinationService : IBusDestinationService
    {
        private IBusDestinationClient _busDestinationClient;

        public BusDestinationService(IBusDestinationClient busDestinationClient)
        {
            _busDestinationClient = busDestinationClient;
        }
        public async Task<IEnumerable<Destination>> GetDestinationsAsync()
        {
            return await _busDestinationClient.GetAllAsync();
        }
    }
}

using BBS.DAL.Clients.Abstract;
using BBS.DAL.Clients.Base;
using BBS.DAL.Constants;
using BBS.Interfaces;
using BBS.Models.ApiModels;
using Microsoft.Extensions.Configuration;

namespace BBS.DAL.Clients.Concrete
{
    public class BusDestinationClient : BaseWebClient<Destination>, IBusDestinationClient
    {
        public BusDestinationClient(ILogger logger, IConfiguration configuration, string route = Routes.BUS_DESTINATION_ROUTE) : base(logger, route, configuration)
        {
        }
    }
}

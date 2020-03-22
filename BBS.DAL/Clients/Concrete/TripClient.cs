using BBS.DAL.Clients.Abstract;
using BBS.DAL.Clients.Base;
using BBS.DAL.Constants;
using BBS.Interfaces;
using BBS.Models.ApiModels;
using Microsoft.Extensions.Configuration;

namespace BBS.DAL.Clients.Concrete
{
    public class TripClient : BaseWebClient<Trip>, ITripClient
    {
        public TripClient(ILogger logger, IConfiguration configuration, string route = Routes.TRIPS_ROUTE) : base(logger, route, configuration)
        {
        }
    }
}

using BBS.DAL.Clients.Abstract;
using BBS.DAL.Clients.Base;
using BBS.DAL.Constants;
using BBS.Interfaces;
using BBS.Models.ApiModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BBS.DAL.Clients.Concrete
{
    public class TicketClient : BaseWebClient<Ticket>, ITicketClient
    {
        public TicketClient(ILogger logger, IConfiguration configuration, string route = Routes.Tickets_ROUTE) : base(logger, route, configuration)
        {

        }
    }
}
